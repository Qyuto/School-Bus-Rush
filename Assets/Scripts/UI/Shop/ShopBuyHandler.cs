using System;
using System.Linq;
using Save;
using Skins;
using UnityEngine;

namespace UI.Shop
{
    public class ShopBuyHandler : MonoBehaviour
    {
        [SerializeField] private ShopUI shopUI;
        public Action<SkinInfo> onCurrentSkinChange;

        public bool AddNewSkin(SkinInfo info)
        {
            if (info.cost > DataPersistence.Instance.data.totalPassenger) return false;
            PlayerSkinCollection.Instance.AddNewSkin(info);

            SelectCurrentSkin(info);
            DataPersistence.Instance.data.totalPassenger -= info.cost;
            shopUI.UpdateMoneyText(DataPersistence.Instance.data.totalPassenger);
            return true;
        }

        public void SelectCurrentSkin(SkinInfo info)
        {
            PlayerSkinCollection.Instance.SetLastSkin(info);
            onCurrentSkinChange?.Invoke(info);
        }

        public bool CheckSkinPurchased(SkinInfo info)
        {
            SkinData skinData = DataPersistence.Instance.data.skinData;
            switch (info.type)
            {
                case SkinType.Passenger:
                    return skinData.totalPurchasedPassengerSkins.Contains(info.key);
                case SkinType.FinishModel:
                    return skinData.totalPurchasedSchoolSkins.Contains(info.key);
            }

            return false;
        }
    }
}