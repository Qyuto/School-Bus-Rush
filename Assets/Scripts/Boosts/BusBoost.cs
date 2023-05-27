using UnityEngine;

namespace Boosts
{
    public abstract class BusBoost : MonoBehaviour, IInteractable
    {
        [SerializeField] private BoostType boostType;
        public BoostType GetBoost() => boostType;
        public InteractableType GetInteractableType() => InteractableType.Boost;
        public void Select()
        {
            Destroy(gameObject);
        }
    }

    public enum BoostType
    {
        Speed = 0,
        Passenger 
    }
}
