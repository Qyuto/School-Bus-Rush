using Ads;
using UnityEngine;
using UnityEngine.Events;

namespace Bus
{
    public class BusLevelCompletion : MonoBehaviour
    {
        public UnityEvent onBusArrivedAtEnd;
        public UnityEvent onBusLevelComplete;
        public UnityEvent onBusLevelFailComplete;
        public bool isArrived { get; private set; }
        public bool isFailed { get; set; }

        private void Awake()
        {
            onBusArrivedAtEnd.AddListener(() => isArrived = true);
            onBusLevelFailComplete.AddListener(() => isFailed = true);
        }

        private void Start()
        {
#if YandexSDK
#else
        SubscribeForUnityAds();
#endif
        }

        private void OnDestroy()
        {
#if YandexSDK
#else
        UnSubscribeForUnityAds();
#endif
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