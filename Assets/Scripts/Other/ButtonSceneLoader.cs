using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Other
{
    public class ButtonSceneLoader : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(LoadScene);
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}