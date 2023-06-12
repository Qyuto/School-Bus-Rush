using System.Collections.Generic;
using System.Linq;
using Save;
using UnityEngine;

namespace Skins
{
    public class SkinDataCollection : MonoBehaviour, ILoadDataPersistence, ISaveDataPersistence
    {
        [SerializeField] private List<SkinInfo> dataCosmetics;
        [Header("Debug")][SerializeField] protected SkinInfo lastPassengerSkin;
        [SerializeField] protected SkinInfo lastSchoolSKin;

        public SkinInfo passengerSkin => lastPassengerSkin;
        public SkinInfo schoolSkin => lastSchoolSKin;

        private HashSet<string> _totalPassengerSkins;
        private HashSet<string> _totalSchoolSkins;
        public static SkinDataCollection Instance;

        private int _test;
        
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        public void AddNewSkin(SkinInfo info)
        {
            switch (info.type)
            {
                case SkinType.Passenger:
                    _totalPassengerSkins.Add(info.key);
                    break;
                case SkinType.FinishModel:
                    _totalSchoolSkins.Add(info.key);
                    break;
            }
        }
        public void SetLastSkin(SkinInfo info)
        {
            switch (info.type)
            {
                case SkinType.Passenger:
                    lastPassengerSkin = info;
                    break;
                case SkinType.FinishModel:
                    lastSchoolSKin = info;
                    break;
            }
        }

        public void LoadGame(GameData gameData)
        {
            SkinData skinData = gameData.skinData;

            lastPassengerSkin = dataCosmetics.Find(info => info.key == skinData.lastSelectedPassengerSkin);
            lastSchoolSKin = dataCosmetics.Find(info => info.key == skinData.lastSelectedSchoolSkin);
            _totalPassengerSkins = gameData.skinData.totalPurchasedPassengerSkins.ToHashSet();
            _totalSchoolSkins = gameData.skinData.totalPurchasedSchoolSkins.ToHashSet();
        }

        public void SaveGame(ref GameData gameData)
        {
            SkinData skinData = new SkinData();
            skinData.totalPurchasedPassengerSkins = _totalPassengerSkins.ToArray();
            skinData.totalPurchasedSchoolSkins = _totalSchoolSkins.ToArray();
            skinData.lastSelectedSchoolSkin = lastSchoolSKin.key;
            skinData.lastSelectedPassengerSkin = lastPassengerSkin.key;
            gameData.skinData = skinData;
        }
    }
}