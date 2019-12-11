using UnityEngine;

namespace TEDinc.LearningCards
{
    public abstract class CardBasic : ICardBasic
    {
        public readonly string name;

        public string GetField(string key)
        {
            return null;
        }

        public Sprite GetImage()
        {
            return null;
        }

        public CardBasic(string name)
        {
            this.name = name;
        }
    }
}