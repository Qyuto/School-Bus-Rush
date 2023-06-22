using Ads;
using UnityEngine;
using UnityEngine.Advertisements;

[RequireComponent(typeof(AdsToggle))]
public class AdsInitialize : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private bool testMode = true;
    [SerializeField] private string androidGameId = "5322407";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Init();
    }

    private void Init()
    {
        Advertisement.Initialize(androidGameId, testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log($"[ADS] OnInitializationComplete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"[ADS] OnInitializationFailed {error}");
    }
}