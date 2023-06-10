using Save;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class SceneProgressSaver : MonoBehaviour, ISaveDataPersistence
    {
        public void SaveGame(ref GameData gameData)
        {
            gameData.lastLevel = SceneManager.GetActiveScene().buildIndex;
        }
    }
}