using System;
using Boosts;
using UnityEngine;

namespace Bus
{
    public class BoostReceiver : MonoBehaviour
    {
        private PassengerCount _passengerCount;

        private void Awake()
        {
            _passengerCount = GetComponent<PassengerCount>();
        }

        public void ReceiveBoost(BusBoost boost)
        {
            switch (boost.GetBoost())
            {
                case BoostType.Speed:
                    break;
                case BoostType.Passenger:
                    _passengerCount.onBusCollectPassenger?.Invoke(((PassengerBoost)boost).BoostCount);
                    break;
            }
        }
    }
}