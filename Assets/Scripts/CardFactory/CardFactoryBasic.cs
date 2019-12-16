using System.Collections.Generic;

namespace TEDinc.LearningCards
{
    public abstract class CardFactoryBasic : ICardFactoryBasic
    {
        public List<ICardBasic> cards { get; protected set; }

        public virtual void AddCard(ICardBasic card)
        {
            if (cards == null)
                cards = new List<ICardBasic>();

            cards.Add(card);
        }

        public abstract void SelectCard(ICardBasic card);

        public abstract void InteractWithCard(ICardBasic card);

        public abstract void InteractWithSelectedCard();
    }
}