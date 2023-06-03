using Bus;
using UnityEngine;

namespace Obstacles
{
    public class DivideObstacle : Obstacle
    {
        [SerializeField] private float dividePassenger;
        public float DivideValue => dividePassenger;

        public override void Select(IBusInteractor interactor)
        {
            PassengerCount passengerCount = interactor.GetPassengerCountComponent();
            float divide = passengerCount.CurrentPassenger / dividePassenger;
            if (divide < 1) divide = passengerCount.CurrentPassenger;
            passengerCount.onBusLostPassenger?.Invoke((int)divide);

            if (useVFX) Instantiate(vfxExplosion, transform.position, Quaternion.identity).Play();
            onInteractSelect?.Invoke(interactor);
            Destroy(gameObject);
        }
    }
}