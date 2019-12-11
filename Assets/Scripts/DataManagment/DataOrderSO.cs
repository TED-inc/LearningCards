using UnityEngine;

namespace TEDinc.LearningCards
{
    [CreateAssetMenu(fileName = "CardDataOrder", menuName = "Learning Cards/Create CardDataOrderSO")]
    public class DataOrderSO : ScriptableObject
    {
        [SerializeField]
        private string[] keys;
        
        public string[] GetKeys()
        {
            return keys;
        }

        public int GetValue(string key)
        {
            for (int i = 0; i < keys.Length; i++)
                if (keys[i] == key)
                    return i;

            Debug.LogError("No key \"" + key + "\" was found");
            return 0;
        }
    }

    public enum DataOrderCommonTypes
    {
        name,
        imagePath,
        imagePathSubOrder,
        translaton,
        word,
    }
}