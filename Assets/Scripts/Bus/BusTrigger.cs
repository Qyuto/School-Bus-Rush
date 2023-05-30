using UnityEngine;
using UnityEngine.Events;

namespace Bus
{
    public class BusTrigger : MonoBehaviour
    {
        public UnityEvent<GameObject> onTriggerEnter;
        private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag("Player")) return;
            onTriggerEnter?.Invoke(other.gameObject);
        }
    }
}