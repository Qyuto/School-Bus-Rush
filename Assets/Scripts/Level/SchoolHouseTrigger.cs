using UnityEngine;

namespace Level
{
    public class SchoolHouseTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag("Player")) return;
            BusMovement busMovement = other.GetComponentInParent<BusMovement>();
            if(busMovement == null) return;
            busMovement.onBusFinished?.Invoke();
        }
    }
}