using System;

namespace Bus
{
    public class BusRatePassenger
    {
        private int _rateIncreaseThreshold;
        private int _currentRate = 1;
        public int Rate => _currentRate;
        public Action<int, int, int> OnRateChanged;

        public BusRatePassenger(int rateIncreaseThreshold)
        {
            _rateIncreaseThreshold = rateIncreaseThreshold;
        }

        public bool TryAddRate(int size)
        {
            if (_rateIncreaseThreshold > size) return false;
            _rateIncreaseThreshold *= 2;
            _currentRate++;
            if (_currentRate >= 10) _currentRate = 10;
            OnRateChanged?.Invoke(_rateIncreaseThreshold / 2, _rateIncreaseThreshold, _currentRate);
            return true;
        }
    }
}