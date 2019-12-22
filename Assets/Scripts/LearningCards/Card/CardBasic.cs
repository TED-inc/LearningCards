using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using TEDinc.Utils;

namespace TEDinc.LearningCards
{
    [ExecuteInEditMode]
    public abstract class CardBasic : MonoBehaviour, ICardBasic, IPointerClickHandler
    {
        public bool clicable = true;
        public Image imageRenderer;



        public bool valid { get; protected set; }
        public string identifier { get; protected set; }
        public string dataFilePath { get; protected set; }
        public ICardFactoryBasic cardFactory { get; protected set; }
        [TypeConstraint(typeof(ICardInteractorBasic)), SerializeField]
        private MonoBehaviour _cardInteractor;
        public ICardInteractorBasic cardInteractor
        {
            get { return _cardInteractor as ICardInteractorBasic; }
            set { _cardInteractor = value as MonoBehaviour; }
        }
        protected Sprite sprite;



        public string GetField(string key)
        {
            if (valid)
                return GetField(DataLoadController.dataOrderSO.GetKeyIndex(key));
            else 
                return null;
        }

        public string GetField(int index)
        {
            if (valid)
                return DataLoadController.GetCardDataField(identifier, dataFilePath, index);
            else
                return null;
        }

        public string GetField(DataOrderCommonTypes dataOrder)
        {
            if (valid)
                return DataLoadController.GetCardDataField(identifier, dataFilePath, dataOrder);
            else
                return null;
        }

        public string[] GetData()
        {
            return DataLoadController.GetCardData(identifier, dataFilePath);
        }

        public Sprite GetSprite()
        {
            if (sprite == null && valid)
            {
                string spritePath = GetField(DataOrderCommonTypes.spritePath);
                int spritePathSubOrder = Int32.Parse(GetField(DataOrderCommonTypes.spritePathSubOrder));

                if (!File.Exists(spritePath))
                    Debug.LogError('[' + GetType().Name + "]\nNo sprite by path " + spritePath);
                else
                {
                    try
                    {
                        sprite = AssetDatabase.LoadAllAssetsAtPath(spritePath)[spritePathSubOrder] as Sprite;
                    }
                    catch
                    {
                        Debug.LogError('[' + GetType().Name + "]\nNo subfiles by path " + spritePath + '[' + spritePathSubOrder + ']');
                        sprite = null;
                    }
                }
            }

            return sprite;
        }

        public void Setup(string identifier, string dataFilePath, ICardFactoryBasic cardFactory)
        {
            this.identifier = identifier;
            this.dataFilePath = dataFilePath;
            this.cardFactory = cardFactory;
            sprite = null;
            if (CheckValidation())
                Setup();
        }



        public virtual void Interact()
        {
            if (cardFactory != null && valid)
                cardFactory.InteractWithCard(this);
            if (cardInteractor != null && valid)
                cardInteractor.Interact();
        }

        public virtual bool CheckValidation()
        {
            valid = GetData() != null;
            return valid;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (clicable)
                Interact();
        }



        protected virtual void Setup()
        {
            if (imageRenderer != null)
                imageRenderer.sprite = GetSprite();
            if (cardInteractor != null)
                cardInteractor.Revert();
        }



        public CardBasic()
        {
            valid = false;
        }

        public CardBasic(string identifier, string dataFilePath, ICardFactoryBasic cardFactory)
        {
            Setup(identifier, dataFilePath, cardFactory);
        }
    }
}