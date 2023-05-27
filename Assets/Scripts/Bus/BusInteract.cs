using System;
using Boosts;
using UnityEngine;

namespace Bus
{
    public class BusInteract : MonoBehaviour
    {
        [SerializeField] private Vector3 searchExtend;
        [SerializeField] private LayerMask interactableMask;
        
        private readonly Collider[] _overlapFind = new Collider[10];
        private PassengerCount _passengerCount;
        private BoostReceiver _boostReceiver;

        private void Awake()
        {
            _passengerCount = GetComponent<PassengerCount>();
            _boostReceiver = GetComponent<BoostReceiver>();
        }

        private void Update()
        {
            TryFindInteractable();
        }

        private void TryFindInteractable()
        {
            int interactableSize = Physics.OverlapBoxNonAlloc(transform.position, searchExtend / 2, _overlapFind, transform.rotation, interactableMask);
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
                case InteractableType.Boost:
                    _boostReceiver.ReceiveBoost((BusBoost)(interactable));
                    break;
            }
            interactable.Select();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position,searchExtend);
        }
    }
}