using Ads;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Bus
{
    public class BusLevelCompletion : MonoBehaviour
    {
        public UnityEvent onBusArrivedAtEnd;
        public UnityEvent onBusLevelComplete;
        public UnityEvent onBusLevelFailComplete;
        public bool isArrived { get; private set; }

        private void Awake()
        {
            onBusArrivedAtEnd.AddListener(() => isArrived = true);
        }

        private void Start()
        {
#if YandexSDK
            SubscribeForYandex();
#else
        SubscribeForUnityAds();
#endif
        }

        private void OnDestroy()
        {
#if YandexSDK
            UnSubscribeForYandex();
#else
        UnSubscribeForUnityAds();
#endif
        }

        private void SubscribeForYandex()
        {
            onBusArrivedAtEnd.AddListener(ShowYandexAdsVideo);
        }

        private void ShowYandexAdsVideo()
        {
            if (YandexGame.Instance != null)
                YandexGame.Instance._FullscreenShow();
        }

        private void UnSubscribeForYandex()
        {
            onBusArrivedAtEnd.RemoveListener(ShowYandexAdsVideo);
        }

        private void SubscribeForUnityAds()
        {
            onBusArrivedAtEnd.AddListener(AdsToggle.Instance.LoadBanner);
            onBusLevelComplete.AddListener(AdsToggle.Instance.LoadInterstitial);
            onBusLevelFailComplete.AddListener(AdsToggle.Instance.LoadInterstitial);
        }

        private void UnSubscribeForUnityAds()
        {
            onBusArrivedAtEnd.RemoveAllListeners();
            onBusLevelComplete.RemoveAllListeners();
            onBusLevelFailComplete.RemoveAllListeners();
        }
    }
}