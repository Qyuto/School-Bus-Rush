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

        private void ReduceSize(int reduceValue)
        {
            _sizeScaler.ReduceScale(((float)reduceValue * scaleFactor),false);
        }

        private void IncreaseSize(int increaseValue)
        {
            _sizeScaler.IncreaseScale((float)increaseValue * scaleFactor,true);
        }
    }
}