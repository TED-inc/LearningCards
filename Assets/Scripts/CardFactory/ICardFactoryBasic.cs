namespace TEDinc.LearningCards
{
    public interface ICardFactoryBasic
    {
        void AddCard(ICardBasic card);
        void SelectCard(ICardBasic card);
        void InteractWithCard(ICardBasic card);
        void InteractWithSelectedCard();
    }
}