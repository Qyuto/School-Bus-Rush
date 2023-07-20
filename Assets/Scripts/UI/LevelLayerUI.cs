using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLayerUI : MonoBehaviour
{
    [SerializeField] private Text moneyText;
    [SerializeField] private Button buttonRestart;

    private void Awake()
    {
        buttonRestart.onClick.AddListener(RestartLevel);
    }

    public void UpdateMoneyText(int size)
    {
        moneyText.text = size.ToString();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}