using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

namespace Modifier
{
    public class PassengerModifier : BusModifier
    {
        [SerializeField] private PassengerModifierType passengerBoostType = PassengerModifierType.Increase;
        [SerializeField] private int maxBoostValue;
        [SerializeField] private int minBoostValue;
        [SerializeField] private TextMeshPro valueText;

        private int _randomizedValue;
        private void Awake()
        {
            _randomizedValue =  Random.Range(minBoostValue,maxBoostValue);
            string text;
            switch (passengerBoostType)
            {
                case PassengerModifierType.Increase:
                     text = $"+{_randomizedValue.ToString()}";
                    break;
                case PassengerModifierType.Multiplier:
                    text = $"*{_randomizedValue.ToString()}";
                    break;
                case PassengerModifierType.Decrease:
                    text = $"-{_randomizedValue.ToString()}";
                    break;
                case PassengerModifierType.Divid:
                    text = $"/{_randomizedValue.ToString()}";
                    break;
                default:
                     text = $"None";
                    break;

            }
            valueText.text = text;
        }

        public PassengerModifierType ModType => passengerBoostType;
        public int PassengerCount => _randomizedValue;
    }

    public enum PassengerModifierType
    {
        Increase = 0,
        Decrease,
        Multiplier,
        Divid,
    }

}