using Save;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelLoad : MonoBehaviour,ILoadDataPersistence
    {
        public void LoadGame(GameData gameData)
        {
            SceneManager.LoadScene(gameData.lastLevel);
        }
    }
}