using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Bus
{
    public class PassengerCount : MonoBehaviour
    {
        [SerializeField] private int currentPassenger;
        [SerializeField] private TextMeshPro busPassengerCountText;

        public UnityEvent<int> onBusCollectPassenger;
        public UnityEvent<int> onBusLostPassenger;
        public int CurrentPassenger => currentPassenger;

        private void Awake()
        {
            onBusCollectPassenger.AddListener(OnCollectPeople);
            onBusLostPassenger.AddListener(OnLostPeople);
        }

        private void OnLostPeople(int removePeopleSize)
        {
            currentPassenger -= removePeopleSize;
            busPassengerCountText.text = currentPassenger.ToString();
            if (currentPassenger <= 0) Debug.Log($"BUS: Dont have passengers");
        }

        private void OnCollectPeople(int addPeopleSize)
        {
            currentPassenger += addPeopleSize;
            busPassengerCountText.text = currentPassenger.ToString();
        }
    }
}