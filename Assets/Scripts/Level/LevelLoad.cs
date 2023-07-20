using Save;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelLoad
    {
        private readonly GameData _gameData;

        public LevelLoad(GameData gameData)
        {
            _gameData = gameData;
        }

        public void LoadNextLevel()
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings - 1) _gameData.playerFinishedGame = true;

            if (!DataPersistence.Instance.data.playerFinishedGame) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else SceneManager.LoadScene(Random.Range(1, SceneManager.sceneCountInBuildSettings - 1));
        }

        public void LoadLastSavedLevel()
        {
            SceneManager.LoadScene(_gameData.lastLevel);
        }
    }
}