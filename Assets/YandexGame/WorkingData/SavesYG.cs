using Save;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public int totalPassenger;
        public int lastLevel;
        public SkinData skinData;
        public string lastGameVersion;

        public SavesYG()
        {
            totalPassenger = 0;
            lastLevel = 1;
            lastGameVersion = "0.0.0.0";
            skinData = new SkinData();
        }
    }
}