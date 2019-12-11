using System;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace TEDinc.LearningCards
{
    public abstract class CardBasic : ICardBasic
    {
        public readonly string name;
        public readonly string dataFilePath;

        protected Sprite image;

        public string GetField(string key)
        {
            return DataLoadController.GetCardData(name, dataFilePath)[DataLoadController.dataOrderSO.GetKeyIndex(key)];
        }

        public Sprite GetImage()
        {
            if (image == null)
                {
                    string[] data = DataLoadController.GetCardData(name, dataFilePath);
                    if (ValidateDataBeforeSetAnImage(data))
                        image = AssetDatabase.LoadAllAssetsAtPath(data[1])[Int32.Parse(data[2])] as Sprite;
                }

            return image;
        }  

        public abstract void Interact();



        private bool ValidateDataBeforeSetAnImage(string[] data)
        {
            if (data == null)
                Debug.LogError("No Data");
            else if (!File.Exists(data[(int)DataOrderCommonTypes.imagePath]))
                Debug.LogError("No image file");
            else
                return true;

            return false;
        }



        public CardBasic(string name, string dataFilePath)
        {
            this.name = name;
            this.dataFilePath = dataFilePath;
        }
    }
}