using UnityEngine;

namespace TEDinc.LearningCards
{
    [CreateAssetMenu(fileName = "CardDataOrder", menuName = "Learning Cards/Create CardDataOrderSO")]
    public class DataOrderSO : ScriptableObject
    {
        [SerializeField]
        private string[] keys = null;
        
        public string[] GetKeys()
        {
            return keys;
        }

        public int GetKeyIndex(string key)
        {
            for (int i = 0; i < keys.Length; i++)
                if (keys[i] == key)
                    return i;

            Debug.LogError('[' + GetType().ToString() + "]\nNo key \"" + key + "\" was found");
            return 0;
        }
    }

    public enum DataOrderCommonTypes
    {
        identifier,
        spritePath,
        spritePathSubOrder,
        word,
        translation,
    }
}