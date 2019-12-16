using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace TEDinc.LearningCards
{
    public class TestCardsDataManager : MonoBehaviour
    {
        public string identifier;
        public string path = "cccc";
        public Image image;
        public UnityEngine.Object file;

        [ContextMenu("Test")]
        private void Test()
        {
            string[] data = DataLoadController.GetCardData(identifier, path);
            if (ValidateDataBeforeSetAnImage(data))
                image.sprite = AssetDatabase.LoadAllAssetsAtPath(data[1])[Int32.Parse(data[2])] as Sprite;
        }

        private bool ValidateDataBeforeSetAnImage(string[] data)
        {
            if (data == null)
                Debug.LogError("No Data");
            else if (!File.Exists(data[(int)DataOrderCommonTypes.spritePath]))
                Debug.LogError("No image file");
            else
                return true;

            return false;
        }
                
        [ContextMenu("SetFilePass")]
        private void SetFilePass()
        {
            path = AssetDatabase.GetAssetPath(file);
        }
    }
}