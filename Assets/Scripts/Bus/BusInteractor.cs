using UnityEngine;

namespace Bus
{
    public class BusInteractor : MonoBehaviour, IBusInteractor
    {
        [SerializeField] private Vector3 searchExtent;
        [SerializeField] private LayerMask interactableMask;

        private readonly Collider[] _overlapFind = new Collider[10];
        private PassengerCount _passengerCount;
        private ModifierReceiver _modReceiver;
        private BusLevelCompletion _levelCompletion;
        private BusRatePassenger _ratePassenger;
        
        private void Awake()
        {
            _levelCompletion = GetComponent<BusLevelCompletion>();
            _passengerCount = GetComponent<PassengerCount>();
            _modReceiver = GetComponent<ModifierReceiver>();
            BusLevelCompletion levelCompletion = GetComponent<BusLevelCompletion>();
            levelCompletion.onBusArrivedAtEnd.AddListener(DisableComponent);
        }

        private void DisableComponent()
        {
            this.enabled = false;
        }

        private void Update()
        {
            TryFindInteractable();
        }

        private void TryFindInteractable()
        {
            int interactableSize = Physics.OverlapBoxNonAlloc(transform.position,
                Vector3.Scale(searchExtent / 2, transform.localScale), _overlapFind, transform.rotation,
                interactableMask, QueryTriggerInteraction.Ignore);
            if (interactableSize == 0) return;

            for (int i = 0; i < interactableSize; i++)
            {
                IInteractable interactable = _overlapFind[i].GetComponentInParent<IInteractable>();
                if (interactable == null) continue;
                Interact(interactable);
            }
        }

        private void Interact(IInteractable interactable)
        {
            interactable.Select(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, searchExtent);
        }

        public PassengerCount GetPassengerCountComponent() => _passengerCount;
        public ModifierReceiver GetModifierReceiver() => _modReceiver;
        public BusLevelCompletion GetBusLevelCompletion() => _levelCompletion;
        public BusRatePassenger GetBusRatePassenger() => _ratePassenger;

        public Transform GetTransform() => transform;
    }
}