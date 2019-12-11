namespace TEDinc.LearningCards
{
    public interface ICardsFactoryBasic
    {
        void AddCard(ICardBasic card);
        void SelectCard(ICardBasic card);
        void InteractWithSelectedCard();
    }
}