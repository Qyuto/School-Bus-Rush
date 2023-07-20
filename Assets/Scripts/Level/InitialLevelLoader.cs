using Save;
using UnityEngine;

namespace Level
{
    public class InitialLevelLoader : MonoBehaviour, ILoadDataPersistence
    {
        public void LoadGame(GameData gameData)
        {
            LevelLoad levelLoad = new LevelLoad(gameData);
            levelLoad.LoadLastSavedLevel();
        }
    }
}