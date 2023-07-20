using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Bus;
using Level;
using Save;
using Random = UnityEngine.Random;

public class FinishLevelUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup finishGroup;
    [SerializeField] private CanvasGroup looseGroup;

    [SerializeField] private Text passengerCountText;
    [SerializeField] private BusLevelCompletion busLevelCompletion;
    [SerializeField] private PassengerCount passengerCount;
    [Header("Buttons")] [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button restartLevelButton;

    private void Awake()
    {
        busLevelCompletion.onBusLevelComplete.AddListener(ShowUI);
        busLevelCompletion.onBusLevelFailComplete.AddListener(ShowLooseUI);

        finishGroup.gameObject.SetActive(false);
        nextLevelButton.onClick.AddListener(LoadNewLevel);
        restartLevelButton.onClick.AddListener(RestartLevel);
    }

    private void ShowLooseUI()
    {
        looseGroup.transform.parent.gameObject.SetActive(true);
        looseGroup.gameObject.SetActive(true);
        nextLevelButton.interactable = false;
        Text text = nextLevelButton.GetComponentInChildren<Text>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0.25f);
    }

    private void ShowUI()
    {
        nextLevelButton.interactable = true;
        finishGroup.transform.parent.gameObject.SetActive(true);
        finishGroup.gameObject.SetActive(true);
    }

    private void LoadNewLevel()
    {
        LevelLoad levelLoad = new LevelLoad(DataPersistence.Instance.data);
        levelLoad.LoadNextLevel();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}