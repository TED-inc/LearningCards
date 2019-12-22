using UnityEngine;

namespace TEDinc.LearningCards
{
    public class TestCardBasicInteractorGet : MonoBehaviour
    {
        public CardBasic cardBasic;

        [ContextMenu("GetInteractor")]
        public void GetInteractor()
        {
            if (cardBasic.cardInteractor != null)
                cardBasic.cardInteractor.Interact();
        }
    }
}