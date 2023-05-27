using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

namespace Boosts
{
    public class PassengerBoost : BusBoost
    {
        [SerializeField] private int maxBoostValue;
        [SerializeField] private int minBoostValue;
        [SerializeField] private TextMeshPro valueText;

        private int _randomizedValue;
        private void Awake()
        {
            _randomizedValue =  Random.Range(minBoostValue,maxBoostValue);
            valueText.text = $"+{_randomizedValue.ToString()}";
        }

        public int BoostCount => _randomizedValue;
    }
}