using Other;
using Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class ShopUI : MonoBehaviour, ILoadDataPersistence
    {
        [SerializeField] private Text moneyText;
        [SerializeField] private Button resumeGame;
        [SerializeField] private LoadLastLevel loadLastLevel;
        [SerializeField] private Button openPassenger;
        [SerializeField] private GameObject passengerLayer;
        [SerializeField] private Button openSchool;
        [SerializeField] private GameObject schoolLayer;

        private void Awake()
        {
            resumeGame.onClick.AddListener(ResumeGame);
            schoolLayer.SetActive(false);
            passengerLayer.SetActive(true);

            openPassenger.onClick.AddListener(EnablePassengerLayer);
            openSchool.onClick.AddListener(EnableSchoolLayer);
        }

        private void EnableSchoolLayer()
        {
            passengerLayer.SetActive(false);
            schoolLayer.SetActive(true);
        }

        private void EnablePassengerLayer()
        {
            schoolLayer.SetActive(false);
            passengerLayer.SetActive(true);
        }

        public void UpdateMoneyText(int value)
        {
            moneyText.text = value.ToString();
        }

        private void ResumeGame()
        {
            loadLastLevel.Load(DataPersistence.Instance.data.lastLevel);
        }

        public void LoadGame(GameData gameData)
        {
            UpdateMoneyText(gameData.totalPassenger);
        }
    }
}