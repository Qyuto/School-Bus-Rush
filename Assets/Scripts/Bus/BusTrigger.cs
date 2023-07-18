using UnityEngine;
using UnityEngine.Events;

namespace Bus
{
    public class BusTrigger : MonoBehaviour
    {
        private Collider _collider;
        public UnityEvent<GameObject> onTriggerEnter;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            onTriggerEnter?.Invoke(other.gameObject);
        }

        private void OnDisable()
        {
            if (_collider != null) _collider.enabled = false;
        }

        private void OnEnable()
        {
            if (_collider != null) _collider.enabled = true;
        }

        private void OnDestroy()
        {
            onTriggerEnter.RemoveAllListeners();
        }
    }
}