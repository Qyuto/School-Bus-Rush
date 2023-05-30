using Bus;
using UnityEngine;

namespace Level
{
    public class SchoolHouse : MonoBehaviour
    {
        [SerializeField] private int minPassengerCount;
        [SerializeField] private BusTrigger busTrigger;

        private void Awake()
        {
            busTrigger.onTriggerEnter.AddListener(OnPlayerFinished);
        }

        private void OnPlayerFinished(GameObject bus)
        {
            PassengerCount passengerCount = bus.GetComponentInParent<PassengerCount>();
            BusMovement movement = passengerCount.GetComponent<BusMovement>();
            if (passengerCount == null || movement == null) return;
            if (minPassengerCount <= passengerCount.CurrentPassenger) movement.onBusFinished?.Invoke();
            else movement.onBusFailFinished?.Invoke();
        }
    }
}