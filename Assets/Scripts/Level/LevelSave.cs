using Save;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelSave : MonoBehaviour, ISaveDataPersistence
    {
        public void SaveGame(ref GameData gameData)
        {
            gameData.lastLevel = SceneManager.GetActiveScene().buildIndex;
        }
    }
}