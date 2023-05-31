using Modifier;
using Obstacles;
using UnityEngine;

namespace Bus
{
    public class BusInteract : MonoBehaviour
    {
        [SerializeField] private Vector3 searchExtend;
        [SerializeField] private LayerMask interactableMask;
        
        private readonly Collider[] _overlapFind = new Collider[10];
        private PassengerCount _passengerCount;
        private ModifierReceiver _boostReceiver;

        private void Awake()
        {
            _passengerCount = GetComponent<PassengerCount>();
            _boostReceiver = GetComponent<ModifierReceiver>();
        }

        private void Update()
        {
            TryFindInteractable();
        }

        private void TryFindInteractable()
        {
            int interactableSize = Physics.OverlapBoxNonAlloc(transform.position, searchExtend / 2, _overlapFind, transform.rotation, interactableMask, QueryTriggerInteraction.Ignore);
            if (interactableSize == 0) return;

            for (int i = 0; i < interactableSize; i++)
            {
                IInteractable interactable = _overlapFind[i].GetComponentInParent<IInteractable>();
                if(interactable == null) continue;
                Interact(interactable);
            }
        }

        private void Interact(IInteractable interactable)
        {
            switch (interactable.GetInteractableType())
            {
                case InteractableType.Passenger:
                    _passengerCount.onBusCollectPassenger?.Invoke(((GroupPeople)interactable).PeopleCount);
                    break;
                case InteractableType.Obstacle:
                    int divide = _passengerCount.CurrentPassenger / ((Obstacle)(interactable)).DividePassenger;
                    _passengerCount.onBusLostPassenger?.Invoke(divide);
                    break;
                case InteractableType.Modifier:
                    _boostReceiver.ReceiveModifier((BusModifier)(interactable));
                    break;
            }
            interactable.Select(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position,searchExtend);
        }
    }
}