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
            _sizeScaler.ReduceScale(((float)modifier.GetPassengerCount() * scaleFactor), false);
        }

        private void IncreaseSize(IPassengerModifier modifier)
        {
            if (transform.localScale.y > 1.5f) return;
            float size = (float)modifier.GetPassengerCount() * scaleFactor;
            Vector3 vectorSize = new Vector3(size, size, size) + transform.localScale;
            if (vectorSize.y > 1.5f) size = vectorSize.y - 1.5f;

            _sizeScaler.IncreaseScale(size, true);
        }
    }
}