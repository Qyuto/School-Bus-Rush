using Modifier;
using UnityEngine;

namespace Bus
{
    public class ModifierReceiver : MonoBehaviour
    {
        private PassengerCount _passengerCount;

        private void Awake()
        {
            _passengerCount = GetComponent<PassengerCount>();
        }

        public void ReceiveModifier(BusModifier modifier)
        {
            switch (modifier.Type)
            {
                case ModifierType.Passenger:
                    PassengerModifier((PassengerModifier)modifier);
                    break;
            }
        }

        private void PassengerModifier(PassengerModifier modifier)
        {
            switch (modifier.ModType)
            {
                case PassengerModifierType.Increase:
                    _passengerCount.onBusCollectPassenger?.Invoke(modifier.PassengerCount);
                    break;
                case PassengerModifierType.Multiplier:
                    int mValue = modifier.PassengerCount * _passengerCount.CurrentPassenger - _passengerCount.CurrentPassenger;
                    _passengerCount.onBusCollectPassenger?.Invoke(mValue);
                    break;
                case PassengerModifierType.Decrease:
                    _passengerCount.onBusLostPassenger?.Invoke(modifier.PassengerCount);
                    break;
                case PassengerModifierType.Divid:
                    int dValue = _passengerCount.CurrentPassenger - (_passengerCount.CurrentPassenger / modifier.PassengerCount);
                    _passengerCount.onBusLostPassenger?.Invoke(dValue);
                    break;
            }
        }
    }
}