using System;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace TEDinc.LearningCards
{
    public static class DataLoadController
    {
        public static DataOrderSO dataOrderSO;

        private static string dataFilePath;
        private static string[][] dataFromFile;



        public static string[] GetCardData(string identifier, string dataFilePath, bool forceLoad = false)
        {
            if (!ValidateParams(identifier, dataFilePath))
                return null;

            if ((DataLoadController.dataFilePath != dataFilePath) || forceLoad)
                LoadTextAsset(dataFilePath);

            return GenerateCardData(identifier);
        }

        public static string GetCardDataField(string identifier, string dataFilePath, int index, bool forceLoad = false)
        {
            string[] data = GetCardData(identifier, dataFilePath, forceLoad);

            if (data != null)
                return data[index];

            return null;
        }

        public static string GetCardDataField(string identifier, string dataFilePath, DataOrderCommonTypes dataOrder, bool forceLoad = false)
        {
            return GetCardDataField(identifier, dataFilePath, (int)dataOrder, forceLoad);
        }



        private static bool ValidateParams(string name, string dataFilePath)
        {
            if (dataOrderSO == null)
                FindDataOrderSO();
            if (name == null || name == "")
                Debug.LogError('[' + typeof(DataLoadController).Name + "]\nEmpty name is not allowed");
            else if (!File.Exists(dataFilePath))
                Debug.LogError('[' + typeof(DataLoadController).Name + "]\nFile path not exist\n" + dataFilePath);
            else
                return true;

            return false;
        }

        private static void FindDataOrderSO()
        {
            string[] soGuids = AssetDatabase.FindAssets("t:scriptableObject", new string[] { "Assets" });
            foreach (string soGuid in soGuids)
            {
                ScriptableObject so =
                    AssetDatabase.LoadAssetAtPath<ScriptableObject>(
                        AssetDatabase.GUIDToAssetPath(soGuid));
                if (so.GetType() == typeof(DataOrderSO))
                {
                    dataOrderSO = so as DataOrderSO;
                    return;
                }
            }

            Debug.LogError('[' + typeof(DataLoadController).Name + "]\nDataOrderSO was not found");
        }

        private static void LoadTextAsset(string dataFilePath)
        {
            DataLoadController.dataFilePath = dataFilePath;
            string[] dataFromFileByLines = File.ReadAllText(dataFilePath).Split('\n');

            dataFromFile = new string[dataFromFileByLines.Length][];
            for (int i = 0; i < dataFromFile.Length; i++)
                dataFromFile[i] = dataFromFileByLines[i].Split(',');
        }

        private static string[] GenerateCardData(string name)
        {
            foreach (string[] line in dataFromFile)
                if (line[(int)DataOrderCommonTypes.identifier] == name)
                    return line;

            Debug.LogError('[' + typeof(DataLoadController).Name + "]\nNo cards with name: " + name);
            return null;
        }
    }
}