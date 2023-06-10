using System;
using System.Collections.Generic;
using System.Linq;
using Save;
using Skins;
using UnityEngine;

namespace UI.Shop
{
    public class ShopBuyHandler : SkinDataCollection, ISaveDataPersistence
    {
        [SerializeField] private ShopUI shopUI;
        public Action<SkinInfo> onCurrentSkinChange;

        private HashSet<string> _totalPassengerSkins;
        private HashSet<string> _totalSchoolSkins;

        public bool AddNewSkin(SkinInfo info)
        {
            if (info.cost > DataPersistence.Instance.data.totalPassenger) return false;
            switch (info.type)
            {
                case SkinType.Passenger:
                    _totalPassengerSkins.Add(info.key);
                    break;
                case SkinType.FinishModel:
                    _totalSchoolSkins.Add(info.key);
                    break;
                default: return false;
            }

            SelectCurrentSkin(info);
            DataPersistence.Instance.data.totalPassenger -= info.cost;
            shopUI.UpdateMoneyText(DataPersistence.Instance.data.totalPassenger);
            return true;
        }

        public void SelectCurrentSkin(SkinInfo info)
        {
            switch (info.type)
            {
                case SkinType.Passenger:
                    GameSkinData.lastSelectedPassengerSkin = info.key;
                    break;
                case SkinType.FinishModel:
                    GameSkinData.lastSelectedSchoolSkin = info.key;
                    break;
            }

            onCurrentSkinChange?.Invoke(info);
        }

        public bool CheckSkinPurchased(SkinInfo info)
        {
            switch (info.type)
            {
                case SkinType.Passenger:
                    return GameSkinData.totalPurchasedPassengerSkins.Contains(info.key);
                case SkinType.FinishModel:
                    return GameSkinData.totalPurchasedSchoolSkins.Contains(info.key);
            }

            return false;
        }

        public override void LoadGame(GameData gameData)
        {
            base.LoadGame(gameData);
            _totalPassengerSkins = GameSkinData.totalPurchasedPassengerSkins.ToHashSet();
            _totalSchoolSkins = GameSkinData.totalPurchasedSchoolSkins.ToHashSet();
        }

        public void SaveGame(ref GameData gameData)
        {
            GameSkinData.totalPurchasedPassengerSkins = _totalPassengerSkins.ToArray();
            GameSkinData.totalPurchasedSchoolSkins = _totalSchoolSkins.ToArray();
        }
    }
}