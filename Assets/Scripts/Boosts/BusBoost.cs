
namespace Boosts
{
    public abstract class BusBoost : UnityEngine.MonoBehaviour
    {
        public abstract void GetBoost();
    }

    public enum BoostType
    {
        Speed = 0,
        Passenger
    }
}
