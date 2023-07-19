using YG;

namespace Save
{
    public static class YandexSaveLoadAdapter
    {
        public static void SaveForYandex(GameData gameData)
        {
            SavesYG savesYg = YandexGame.savesData;
            savesYg.lastGameVersion = gameData.lastGameVersion;
            savesYg.totalPassenger = gameData.totalPassenger;
            savesYg.lastLevel = gameData.lastLevel;
            savesYg.skinData = gameData.skinData;
        }

        public static GameData ConvertYandexSaveToGameData(SavesYG savesYg)
        {
            GameData newGameData = new GameData("0");
            newGameData.skinData = savesYg.skinData;
            newGameData.lastGameVersion = savesYg.lastGameVersion;
            newGameData.lastLevel = savesYg.lastLevel;
            newGameData.totalPassenger = savesYg.totalPassenger;
            return newGameData;
        }
    }
}