using UnityEngine;

namespace TEDinc.LearningCards
{
    public abstract class CardInteractorBasic : MonoBehaviour, ICardInteractorBasic
    {
        public CardInteractorState state { get; protected set; }
        public CardInteractorStateTransiton stateTransiton { get; protected set; }
        public abstract void Interact();

        public abstract void Revert();
    }

    public enum CardInteractorState
    {
        start,
        intermediate,
        final
    }

    public enum CardInteractorStateTransiton
    {
        ended,
        inProgress
    }
}