using UnityEngine;

namespace Ads
{
    public class YandexAdsToggle : MonoBehaviour
    {
        public static YandexAdsToggle Instance;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }
    }
}