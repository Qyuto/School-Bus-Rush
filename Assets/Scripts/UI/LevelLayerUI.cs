using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLayerUI : MonoBehaviour
{
    [SerializeField] private Button buttonRestart;

    private void Awake()
    {
        buttonRestart.onClick.AddListener(RestartLevel);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}