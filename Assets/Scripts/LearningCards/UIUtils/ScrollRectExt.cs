using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace TEDinc.LearningCards
{
    public class ScrollRectExt : ScrollRect
    {
        public ScrollRectInteractEvent onBeginDrag;
        public ScrollRectInteractEvent onEndDrag;


        
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            onBeginDrag.Invoke(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            onEndDrag.Invoke(eventData);
        }

        protected override void Awake()
        {
            base.Awake();

            if (onBeginDrag == null)
                onBeginDrag = new ScrollRectInteractEvent();
            if (onEndDrag == null)
                onEndDrag = new ScrollRectInteractEvent();
        }
    }

    public class ScrollRectInteractEvent : UnityEvent<PointerEventData>
    {
        public ScrollRectInteractEvent() { }
    }
}