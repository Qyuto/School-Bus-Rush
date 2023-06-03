using UnityEngine;

namespace Modifier
{
    public abstract class BusModifier : MonoBehaviour, IInteractable
    {
        [SerializeField] private ModifierType modifierType;
        public ModifierType Type => modifierType;

        public void Select(IBusInteractor interact)
        {
            interact.GetModifierReceiver().ReceiveModifier(this);
            Destroy(gameObject);
        }
    }

    public enum ModifierType
    {
        Passenger = 0,
    }
}