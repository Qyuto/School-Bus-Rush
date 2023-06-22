using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    public class AdsToggle : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] private string androidBanner = "Banner_Android";
        [SerializeField] private string androidInterstitial = "Interstitial_Android";
        public static AdsToggle Instance;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        }

        public void LoadBanner()
        {
            Advertisement.Banner.Load(androidBanner, new BannerLoadOptions() { loadCallback = ShowBanner });
        }

        public void LoadInterstitial()
        {
            Advertisement.Load(androidInterstitial, this);
        }

        private void ShowBanner()
        {
            Debug.Log("Show banner");
            Advertisement.Banner.Show(androidBanner);
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log(placementId);
            Advertisement.Show(placementId, this);
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
        }

        public void OnUnityAdsShowStart(string placementId)
        {
        }

        public void OnUnityAdsShowClick(string placementId)
        {
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
        }
    }
}