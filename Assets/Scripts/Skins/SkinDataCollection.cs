using System.Collections.Generic;
using Save;
using UnityEngine;

namespace Skins
{
    public class SkinDataCollection : MonoBehaviour, ILoadDataPersistence
    {
        [SerializeField] private List<SkinInfo> dataCosmetics;
        [Header("Debug")] [SerializeField] protected SkinInfo lastPassengerSkin;
        [SerializeField] protected  SkinInfo lastSchoolSKin;

        public SkinInfo passengerSkin => lastPassengerSkin;
        public SkinInfo schoolSkin => lastSchoolSKin;
        public static SkinDataCollection Instance;

        protected SkinData GameSkinData;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        public virtual void LoadGame(GameData gameData)
        {
            GameSkinData = gameData.skinData;

            lastPassengerSkin = dataCosmetics.Find(info => info.key == GameSkinData.lastSelectedPassengerSkin);
            lastSchoolSKin = dataCosmetics.Find(info => info.key == GameSkinData.lastSelectedSchoolSkin);
        }
    }
}