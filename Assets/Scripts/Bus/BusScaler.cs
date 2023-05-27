using UnityEngine;

namespace Bus
{
    public class BusScaler : MonoBehaviour
    {
        [SerializeField] private Transform scaleTransform;
        [Range(0.001f, 0.9f), SerializeField] private float scaleFactor;
        [SerializeField] private PassengerCount passengerCount;

        private void Awake()
        {
            passengerCount.onBusCollectPassenger.AddListener(IncreaseSize);
            passengerCount.onBusLostPassenger.AddListener(ReduceSize);
        }

        private void ReduceSize(int reduceValue)
        {
            scaleTransform.localScale -= scaleTransform.localScale * ((float)reduceValue * scaleFactor);
        }

        private void IncreaseSize(int increaseValue)
        {
            scaleTransform.localScale += scaleTransform.localScale * ((float)increaseValue * scaleFactor);
        }
    }
}