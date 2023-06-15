using UnityEngine;

namespace Other
{
    public class SetLimitFps : MonoBehaviour
    {
        [SerializeField] private int maxFps = 60;
        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = maxFps;
        }
    }
}