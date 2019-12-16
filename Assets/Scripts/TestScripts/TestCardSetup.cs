using UnityEngine;

namespace TEDinc.LearningCards
{
    public class TestCardSetup : MonoBehaviour
    {
        public CardBasic cardBasic;
        public string identifier;
        public string dataFilePath;



        [ContextMenu("Setup Card")]
        public void SetupCard()
        {
            cardBasic.Setup(identifier, dataFilePath, null);
        }
    }
}