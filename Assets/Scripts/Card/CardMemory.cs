using TMPro;
using UnityEngine;

namespace TEDinc.LearningCards
{
    public class CardMemory : CardBasic
    {
        [SerializeField]
        protected TextMeshProUGUI wordLabel;
        [SerializeField]
        protected TextMeshProUGUI transltionLabel;

        public override void Interact()
        {
            ///test
            Debug.Log('[' + GetType().Name + "]\nClick");
        }



        protected override void Setup()
        {
            base.Setup();

            if (wordLabel != null)
                wordLabel.text = GetField(DataOrderCommonTypes.word);

            if (transltionLabel != null)
                transltionLabel.text = GetField(DataOrderCommonTypes.translation);
        }



        public CardMemory() : base() { }

        public CardMemory(string identifier, string dataFilePath, ICardFactoryBasic cardFactory) : 
            base(identifier, dataFilePath, cardFactory) {}
    }
}