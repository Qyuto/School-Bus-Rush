using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Bus;
using TMPro;

public class FinishLevelUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup finishGroup;
    [SerializeField] private TextMeshProUGUI passengerCountText;
    [SerializeField] private BusMovement busMovement;
    [SerializeField] private PassengerCount passengerCount;
    [Header("Buttons")] [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button getDebugInfoButton;
    [SerializeField] private Button restartLevelButton;
    
    private void Awake()
    {
        busMovement.onBusFinished.AddListener(ShowUI);
        finishGroup.gameObject.SetActive(false);
        nextLevelButton.onClick.AddListener(LoadNewLevel);
        restartLevelButton.onClick.AddListener(RestartLevel);
    }

    private void ShowUI()
    {
        finishGroup.gameObject.SetActive(true);
        passengerCountText.text = passengerCount.CurrentPassenger.ToString();
    }
    
    private void LoadNewLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}