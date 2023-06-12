using System;
using System.Collections.Generic;
using System.Linq;
using Save;
using Skins;
using UnityEngine;

namespace UI.Shop
{
    public class ShopBuyHandler : MonoBehaviour, ILoadDataPersistence
    {
        [SerializeField] private ShopUI shopUI;
        public Action<SkinInfo> onCurrentSkinChange;
        private SkinData _skinData;

        public bool AddNewSkin(SkinInfo info)
        {
            if (info.cost > DataPersistence.Instance.data.totalPassenger) return false;
            SkinDataCollection.Instance.AddNewSkin(info);

            SelectCurrentSkin(info);
            DataPersistence.Instance.data.totalPassenger -= info.cost;
            shopUI.UpdateMoneyText(DataPersistence.Instance.data.totalPassenger);
            return true;
        }

        public void SelectCurrentSkin(SkinInfo info)
        {
            SkinDataCollection.Instance.SetLastSkin(info);
            onCurrentSkinChange?.Invoke(info);
        }

        public bool CheckSkinPurchased(SkinInfo info)
        {
            switch (info.type)
            {
                case SkinType.Passenger:
                    return _skinData.totalPurchasedPassengerSkins.Contains(info.key);
                case SkinType.FinishModel:
                    return _skinData.totalPurchasedSchoolSkins.Contains(info.key);
            }

            return false;
        }

        public void LoadGame(GameData gameData)
        {
            _skinData = gameData.skinData;
        }
    }
}