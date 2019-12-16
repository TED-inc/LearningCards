using UnityEngine;

namespace TEDinc.LearningCards
{
    public interface ICardBasic
    {
        Sprite GetSprite();
        string GetField(string key);
        string GetField(int index);
        string GetField(DataOrderCommonTypes dataOrder);
        string[] GetData();
        void Interact();
        void Setup(string identifier, string dataFilePath, ICardFactoryBasic cardFactory);
        bool CheckValidation();
    }
}