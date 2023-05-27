using Bus;
using UnityEngine;

namespace Level
{
    public class SchoolHouseTrigger : MonoBehaviour
    {
        [SerializeField] private int minPassengerCount;
        private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag("Player")) return;
            PassengerCount passengerCount = other.GetComponentInParent<PassengerCount>();
            if(passengerCount == null) return;
            BusMovement busMovement = passengerCount.GetComponent<BusMovement>();
            if(busMovement == null) return;
            busMovement.BusFinished(passengerCount.CurrentPassenger >= minPassengerCount);
        }
    }
}