using UnityEngine;

namespace Modifier
{
    public abstract class BusModifier : MonoBehaviour, IInteractable
    {
        [SerializeField] private ModifierType modifierType;

        public ModifierType Type => modifierType;
        public InteractableType GetInteractableType() => InteractableType.Modifier;
        public void Select()
        {
            Destroy(gameObject);
        }
    }
    public enum ModifierType
    {
        Passenger = 0,
    }
}
