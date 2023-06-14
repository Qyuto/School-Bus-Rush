﻿using Skins;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class ShopButtonCreator : MonoBehaviour
    {
        [SerializeField] private SkinDataBase dataBase;
        [SerializeField] private ButtonSkinInteractor prefabButtonSkinInteractor;
        [SerializeField] private GridLayoutGroup passengerLayerShop;
        [SerializeField] private GridLayoutGroup schoolLayerShop;
        
        private void Awake()
        {
            CreateButtons();
        }

        private void CreateButtons()
        {
            foreach (var skin in dataBase.SkinsData)
            {
                Transform buttonParent = null;
                switch (skin.type)
                {
                    case SkinType.Passenger:
                        buttonParent = passengerLayerShop.transform;
                        break;
                    case SkinType.FinishModel:
                        buttonParent = schoolLayerShop.transform;
                        break;
                }

                var newButton = Instantiate(prefabButtonSkinInteractor, Vector3.zero, prefabButtonSkinInteractor.transform.rotation,
                    buttonParent);
                newButton.InitButton(skin);
            }
        }
    }
}