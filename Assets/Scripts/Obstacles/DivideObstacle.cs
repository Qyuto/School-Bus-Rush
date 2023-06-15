using Bus;
using Passenger;
using UnityEngine;

namespace Obstacles
{
    public class DivideObstacle : Obstacle, IPassengerModifier
    {
        [SerializeField] private int dividePassenger;

        public override void Select(IBusInteractor interactor)
        {
            PassengerCount passengerCount = interactor.GetPassengerCountComponent();
            int divide = passengerCount.CurrentPassenger / dividePassenger;
            if (divide < 1) divide = passengerCount.CurrentPassenger;

            dividePassenger = divide;
            passengerCount.onBusLostPassenger?.Invoke(this);

            if (useVFX) Instantiate(vfxExplosion, transform.position, Quaternion.identity).Play();
            onInteractSelect?.Invoke(interactor);
            Destroy(gameObject);
        }

        public int GetPassengerCount() => dividePassenger;
        public string GetModifierType() => "DivideObstacle";
    }
}