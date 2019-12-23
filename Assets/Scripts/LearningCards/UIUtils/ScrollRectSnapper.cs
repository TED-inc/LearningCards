using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TEDinc.LearningCards
{
    public class ScrollRectSnapper : MonoBehaviour
    {
        [Range(0.001f, 1f)]
        public float snapPower = 0.2f;
        public float snapStartSpeed = 10f;

        [SerializeField]
        private ScrollRectExt scrollRectExt;
        [SerializeField]
        private RectTransform itemsHolder;

        private float[] distancesBetweenItemsToCenter;
        private float[] releativeDistancesBetweenItemsToCenter;
        private float releativDistanceTarget;
        private float prevHolderPositionX;
        private bool isSnapEnabled;
        private bool releativDistanceTargetCalculated;


        public void Setup()
        {
            distancesBetweenItemsToCenter = new float[itemsHolder.childCount];
            releativeDistancesBetweenItemsToCenter = new float[itemsHolder.childCount];

            for (int i = 0; i < itemsHolder.childCount; i++)
                distancesBetweenItemsToCenter[i] = itemsHolder.GetChild(i).localPosition.x;

            float maxDistance = Mathf.Max(distancesBetweenItemsToCenter);
            float minDistance = Mathf.Min(distancesBetweenItemsToCenter);

            itemsHolder.sizeDelta = new Vector2(
                Mathf.Abs(maxDistance) + Mathf.Abs(minDistance) + (scrollRectExt.transform as RectTransform).rect.size.x, 0f);

            for (int i = 0; i < itemsHolder.childCount; i++)
                releativeDistancesBetweenItemsToCenter[i] = Mathf.InverseLerp(minDistance, maxDistance, distancesBetweenItemsToCenter[i]);

            scrollRectExt.onEndDrag.RemoveListener(EndDrag);
            scrollRectExt.onEndDrag.AddListener(EndDrag);
            scrollRectExt.onBeginDrag.RemoveListener(BeginDrag);
            scrollRectExt.onBeginDrag.AddListener(BeginDrag);
        }


        private void BeginDrag(PointerEventData eventData)
        {
            isSnapEnabled = false;
            releativDistanceTargetCalculated = false;
        }
        private void EndDrag(PointerEventData eventData)
        {
            isSnapEnabled = scrollRectExt.horizontalNormalizedPosition > 0f && scrollRectExt.horizontalNormalizedPosition < 1f;
        }

        private void CalcualteReleativeTargetPosition()
        {
            float releativeDistanceDelta;
            float smallestReleativeDistanceDelta = float.MaxValue;

            foreach (float releativeDistance in releativeDistancesBetweenItemsToCenter)
            {
                releativeDistanceDelta = Mathf.Abs(releativeDistance - scrollRectExt.horizontalNormalizedPosition);
                if (releativeDistanceDelta < smallestReleativeDistanceDelta)
                {
                    smallestReleativeDistanceDelta = releativeDistanceDelta;
                    releativDistanceTarget = releativeDistance;
                }
            }

            releativDistanceTargetCalculated = true;
        }

        private void Start()
        {
            Setup();
        }

        private void Update()
        {
            if (isSnapEnabled && releativDistanceTargetCalculated)
                scrollRectExt.horizontalNormalizedPosition = Mathf.Lerp(
                    scrollRectExt.horizontalNormalizedPosition,
                    releativDistanceTarget,
                    snapPower);
        }

        private void LateUpdate()
        {
            float speed = Mathf.Abs(prevHolderPositionX - itemsHolder.position.x);
            if (speed < snapStartSpeed && speed > 0.01f && !releativDistanceTargetCalculated && isSnapEnabled)
                CalcualteReleativeTargetPosition();

            prevHolderPositionX = itemsHolder.position.x;
        }
    }
}