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

        private void Awake()
        {
            onBusArrivedAtEnd.AddListener(() => isArrived = true);
        }

        private void Start()
        {
            onBusArrivedAtEnd.AddListener(AdsToggle.Instance.LoadBanner);
            onBusLevelComplete.AddListener(AdsToggle.Instance.LoadInterstitial);
            onBusLevelFailComplete.AddListener(AdsToggle.Instance.LoadInterstitial);
        }

        private void OnDestroy()
        {
            onBusArrivedAtEnd.RemoveAllListeners();
            onBusLevelComplete.RemoveAllListeners();
            onBusLevelFailComplete.RemoveAllListeners();
        }
    }
}