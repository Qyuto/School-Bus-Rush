using UnityEngine;

namespace Obstacles
{
    internal class InstantLossObject : MonoBehaviour, IInteractable
    {
        public InteractableType GetInteractableType() => InteractableType.LoseObject;

        public void Select(GameObject interactor)
        {
            BusMovement movement = interactor.GetComponentInParent<BusMovement>();
            if (movement == null) return;
            movement.onBusFailFinished?.Invoke();
        }
    }
}
