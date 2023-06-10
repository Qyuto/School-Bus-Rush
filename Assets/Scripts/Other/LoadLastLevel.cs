using Save;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Other
{
    public class LoadLastLevel : MonoBehaviour
    {
        public void Load(int sceneId)
        {
            SceneManager.LoadScene(sceneId);
        }

        public void Load()
        {
            FileDataHandler dataHandler = new FileDataHandler() { fileName = "player.allure" };
            GameData data = null;
            dataHandler.Load(ref data);
            if (data != null) SceneManager.LoadScene(data.lastLevel);
        }
    }
}