using System;

namespace Save
{
    [Serializable]
    public class GameData
    {
        public int totalPassenger;
        public int lastLevel;
        public SkinData skinData;
        public string lastGameVersion;

        public GameData(string gameVersion)
        {
            totalPassenger = 0;
            lastLevel = 0;
            skinData = new SkinData();
            lastGameVersion = gameVersion;
        }
    }
    
    [Serializable]
    public class SkinData
    {
        public int[] totalPurchasedPassengerSkins;
        public string lastSelectedPassengerSkin;

        public int[] totalPurchasedSchoolSkins;
        public string lastSelectedSchoolSkin;

        public SkinData()
        {
            totalPurchasedPassengerSkins = Array.Empty<int>();
            totalPurchasedSchoolSkins = Array.Empty<int>();
            lastSelectedPassengerSkin = "passenger_default";
            lastSelectedSchoolSkin = "school_default";
        }
    }
    
}