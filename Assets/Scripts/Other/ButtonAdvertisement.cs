using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Other
{
    public class ButtonAdvertisement : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(StartFullScreenAds);
        }

        private void StartFullScreenAds()
        {
            if (YandexGame.Instance != null)
                YandexGame.FullscreenShow();
        }
    }
}