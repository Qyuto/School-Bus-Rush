using Modifier;
using Passenger;
using UnityEngine;

namespace Bus
{
    public class ModifierReceiver : MonoBehaviour, IPassengerModifier
    {
        private PassengerCount _passengerCount;
        private int _value;

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
            _value = modifier.PassengerCount;
            switch (modifier.ModType)
            {
                case PassengerModifierType.Increase:
                    _passengerCount.onBusCollectPassenger?.Invoke(this);
                    break;
                case PassengerModifierType.Multiplier:
                    _value = modifier.PassengerCount * _passengerCount.CurrentPassenger -
                             _passengerCount.CurrentPassenger;
                    _passengerCount.onBusCollectPassenger?.Invoke(this);
                    break;
                case PassengerModifierType.Decrease:
                    _passengerCount.onBusLostPassenger?.Invoke(this);
                    break;
                case PassengerModifierType.Divid:
                    _value = _passengerCount.CurrentPassenger - (_passengerCount.CurrentPassenger / _value);
                    _passengerCount.onBusLostPassenger?.Invoke(this);
                    break;
            }
        }

        public int GetPassengerCount() => _value;
        public string GetModifierType() => "Receiver";
    }
}