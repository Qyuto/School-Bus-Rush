using Passenger;
using UnityEngine;

namespace Bus
{
    [RequireComponent(typeof(TransformSizeScaler))]
    public class BusScaler : MonoBehaviour
    {
        [SerializeField] private Transform scaleTransform;
        [Range(0.001f, 0.9f), SerializeField] private float scaleFactor;
        [SerializeField] private PassengerCount passengerCount;

        private TransformSizeScaler _sizeScaler;
                
        private void Awake()
        {
            _sizeScaler = GetComponent<TransformSizeScaler>();
            passengerCount.onBusCollectPassenger.AddListener(IncreaseSize);
            passengerCount.onBusLostPassenger.AddListener(ReduceSize);
        }

        private void ReduceSize(IPassengerModifier modifier)
        {
            _sizeScaler.ReduceScale(((float)modifier.GetPassengerCount() * scaleFactor),false);
        }

        private void IncreaseSize(IPassengerModifier modifier)
        {
            _sizeScaler.IncreaseScale((float)modifier.GetPassengerCount() * scaleFactor,true);
        }
    }
}