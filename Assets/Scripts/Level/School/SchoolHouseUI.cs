using System;
using System.Collections.Generic;
using Bus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Level.School
{
    public class SchoolHouseUI : MonoBehaviour
    {
        [SerializeField] private TextMeshPro passengerCountText;
        [SerializeField] private Slider ratePassengerSlider;
        [SerializeField] private Image frontSliderImage;
        [SerializeField] private TextMeshProUGUI nextRateText;
        [SerializeField] private TextMeshProUGUI prevRateText;
        [SerializeField] private List<RateUIColorSettings> colorSettingsList;
        private BusRatePassenger _ratePassenger;

        public void Init(BusRatePassenger ratePassenger)
        {
            _ratePassenger = ratePassenger;
            _ratePassenger.onRateChanged += UpdateRateInfoUI;
        }

        private void UpdateRateInfoUI(int prevRateThreshold, int nextRateThreshold, int currentRate)
        {
            ratePassengerSlider.minValue = prevRateThreshold;
            ratePassengerSlider.maxValue = nextRateThreshold;
            nextRateText.text = $"X{currentRate + 1}";
            prevRateText.text = $"X{currentRate}";
            SetColorSettings(currentRate);
        }

        public void UpdatePassengerText(int size)
        {
            passengerCountText.text = size.ToString();
        }

        public void UpdateRateSlider(int value)
        {
            ratePassengerSlider.value = value;
        }

        public void SetCurrentRateText(int rate)
        {
            nextRateText.text = $"X{(rate + 1).ToString()}";
            prevRateText.text = $"X{(rate).ToString()}";
        }

        private void SetColorSettings(int settingsIndex)
        {
            if (settingsIndex >= colorSettingsList.Count)
                settingsIndex = colorSettingsList.Count - 1;
            frontSliderImage.color = colorSettingsList[settingsIndex].SlideColor;
            nextRateText.color = prevRateText.color = colorSettingsList[settingsIndex].TextColor;
        }

        private void OnDestroy()
        {
            if (_ratePassenger != null) _ratePassenger.onRateChanged -= UpdateRateInfoUI;
        }
    }

    [Serializable]
    public class RateUIColorSettings
    {
        [SerializeField] private Color32 sliderColor;
        [SerializeField] private Color32 textColor;

        public Color32 SlideColor => sliderColor;
        public Color32 TextColor => textColor;
    }
}