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
            lastLevel = 1;
            skinData = new SkinData();
            lastGameVersion = gameVersion;
        }
    }
    
    [Serializable]
    public class SkinData
    {
        public string[] totalPurchasedPassengerSkins;
        public string lastSelectedPassengerSkin;

        public string[] totalPurchasedSchoolSkins;
        public string lastSelectedSchoolSkin;

        public SkinData()
        {
            lastSelectedPassengerSkin = "passenger_default";
            lastSelectedSchoolSkin = "school_default";

            totalPurchasedPassengerSkins = new string[] { "passenger_default" };
            totalPurchasedSchoolSkins = new string[] { "school_default" };
        }
    }
    
}