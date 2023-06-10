using Save;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Bus
{
    public class PassengerCount : MonoBehaviour, ISaveDataPersistence, ILoadDataPersistence
    {
        [SerializeField] private LevelLayerUI levelLayerUI;
        [SerializeField] private int currentPassenger;
        [SerializeField] private TextMeshPro busPassengerCountText;

        public UnityEvent<int> onBusCollectPassenger;
        public UnityEvent<int> onBusLostPassenger;

        private Animator _busTextAnimator;
        private BusLevelCompletion _levelCompletion;
        public int totalPassenger { get; private set; }
        public int CurrentPassenger => currentPassenger;

        private void Awake()
        {
            onBusCollectPassenger.AddListener(OnCollectPeople);
            onBusLostPassenger.AddListener(OnLostPeople);
            _levelCompletion = GetComponent<BusLevelCompletion>();
            _busTextAnimator = busPassengerCountText.GetComponent<Animator>();
        }

        private void OnLostPeople(int removePeopleSize)
        {
            currentPassenger -= removePeopleSize;
            busPassengerCountText.text = currentPassenger.ToString();
            if (currentPassenger <= 0 && !_levelCompletion.isArrived) _levelCompletion.onBusLevelFailComplete?.Invoke();
            _busTextAnimator.SetTrigger("Bounce");
        }

        private void OnCollectPeople(int addPeopleSize)
        {
            currentPassenger += addPeopleSize;
            busPassengerCountText.text = currentPassenger.ToString();
            _busTextAnimator.SetTrigger("Bounce");
        }

        public void AddTotalPassenger(int size)
        {
            totalPassenger += size;
            levelLayerUI.UpdateMoneyText(totalPassenger);
        }

        private void OnDestroy()
        {
            onBusCollectPassenger.RemoveAllListeners();
            onBusCollectPassenger.RemoveAllListeners();
        }

        public void LoadGame(GameData gameData)
        {
            totalPassenger = gameData.totalPassenger;
            levelLayerUI.UpdateMoneyText(totalPassenger);
        }

        public void SaveGame(ref GameData gameData)
        {
            gameData.totalPassenger = totalPassenger;
        }
    }
}