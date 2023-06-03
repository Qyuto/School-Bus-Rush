using System;
using UnityEngine;

namespace Bus
{
    public class BusRatePassenger : MonoBehaviour
    {
        [SerializeField] private int rateIncreaseThreshold;

        private int _currentRate = 1;
        public int Rate => _currentRate;

        public Action<int, int, int> onRateChanged;

        public bool TryAddRate(int size)
        {
            if (rateIncreaseThreshold > size) return false;
            rateIncreaseThreshold *= 2;
            _currentRate++;
            if (_currentRate >= 10) _currentRate = 10;
            onRateChanged?.Invoke(rateIncreaseThreshold / 2, rateIncreaseThreshold, _currentRate);
            return true;
        }
    }
}