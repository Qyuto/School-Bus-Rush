using UnityEngine;

namespace Bus
{
    public class PassengerCount : MonoBehaviour
    {
        [SerializeField] private int currentPassenger;
        [SerializeField] private BusCollector busCollector;
        public int CurrentPassenger => currentPassenger;

        private void Awake()
        {
            busCollector.onBusCollectPassenger.AddListener(OnCollectPeople);
            busCollector.onBusLostPassenger.AddListener(OnLostPeople);
        }

        private void OnLostPeople(int removePeopleSize)
        {
            currentPassenger -= removePeopleSize;

            if (currentPassenger <= 0) Debug.Log($"BUS: Dont have passengers");
        }

        private void OnCollectPeople(int addPeopleSize)
        {
            currentPassenger += addPeopleSize;
        }
    }
}