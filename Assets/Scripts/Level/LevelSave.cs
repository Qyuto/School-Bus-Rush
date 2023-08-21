using Bus;
using Save;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelSave : MonoBehaviour, ISaveDataPersistence
    {
        [SerializeField] private BusLevelCompletion busLevelCompletion;

        private bool _saveNextLevel;

        private void Awake()
        {
            busLevelCompletion.onBusLevelComplete.AddListener(OnLevelComplete);
        }

        private void OnLevelComplete()
        {
            _saveNextLevel = true;
        }

        public void SaveGame(ref GameData gameData)
        {
            int saveBuildIndex = _saveNextLevel ? SceneManager.GetActiveScene().buildIndex + 1 : SceneManager.GetActiveScene().buildIndex;
            gameData.lastLevel = saveBuildIndex;
            Debug.Log($"Save next level: {_saveNextLevel}");
        }
    }
}