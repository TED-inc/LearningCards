using UnityEngine;

namespace TEDinc.LearningCards
{
    public interface ICardBasic
    {
        Sprite GetImage();
        string GetField(string key);
    }
}