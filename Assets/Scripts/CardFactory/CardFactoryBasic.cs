using System.Collections.Generic;
using UnityEngine;

namespace TEDinc.LearningCards
{
    public abstract class CardFactoryBasic : ICardFactoryBasic
    {
        protected List<ICardBasic> cards;

        public void AddCard(ICardBasic card)
        {
            if (cards == null)
                cards = new List<ICardBasic>();

            cards.Add(card);
        }

        public void SelectCard(ICardBasic card)
        {
            throw new System.NotImplementedException();
        }

        public void InteractWithSelectedCard()
        {
            throw new System.NotImplementedException();
        }
    }
}