using Passenger;
using UnityEngine;

namespace Bus
{
    [RequireComponent(typeof(TransformSizeScaler))]
    public class BusScaler : MonoBehaviour
    {
        [Range(0.001f, 0.9f), SerializeField] private float scaleFactor;
        [SerializeField] private PassengerCount passengerCount;

        private float _maxScaleAmount = 0.5f;
        private float _minScaleAmount = 0.5f;
        private TransformSizeScaler _sizeScaler;

        private void Awake()
        {
            _sizeScaler = GetComponent<TransformSizeScaler>();
            passengerCount.onBusCollectPassenger.AddListener(IncreaseScale);
            passengerCount.onBusLostPassenger.AddListener(ReduceScale);
        }

        private void ReduceScale(IPassengerModifier modifier)
        {
            float size = modifier.GetPassengerCount() * scaleFactor;
            ValidateScaleValue(ref size, ref _minScaleAmount);
            if (size > 0) _sizeScaler.ReduceScale(size, false);
            _maxScaleAmount += size;
            if (_maxScaleAmount > 0.5) _maxScaleAmount = 0.5f;
        }

        private void IncreaseScale(IPassengerModifier modifier)
        {
            float size = modifier.GetPassengerCount() * scaleFactor;
            ValidateScaleValue(ref size, ref _maxScaleAmount);
            if (size > 0) _sizeScaler.IncreaseScale(size, true);
            _minScaleAmount += size;
            if (_minScaleAmount > 0.5) _minScaleAmount = 0.5f;
        }

        private void ValidateScaleValue(ref float value, ref float amount)
        {
            amount -= value;
            if (amount < 0)
            {
                amount += value;
                value = amount;
                amount -= value;
            }
        }
    }
}