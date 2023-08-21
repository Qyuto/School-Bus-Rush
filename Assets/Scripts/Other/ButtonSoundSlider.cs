using Bus;
using UnityEngine;
using UnityEngine.UI;

namespace Other
{
    public class ButtonSoundSlider : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Text soundText;
        [SerializeField] private BusSound audioSource;
        private Button _buttonToggleSound;
        private static float _volume;

        private void Awake()
        {
            slider.onValueChanged.AddListener(UpdateSoundVolume);
            slider.value = _volume;
        }

        private void Start()
        {
            audioSource.SetSoundVolume(_volume);
        }

        private void UpdateSoundVolume(float value)
        {
            _volume = value;
            audioSource.SetSoundVolume(value);
            soundText.text = value.ToString("0.00");
        }
    }
}