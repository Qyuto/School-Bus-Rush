using TMPro;
using UnityEngine;

public class DebugFps : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;

    private void LateUpdate()
    {
        fpsText.text = $"FPS: {(1f / Time.smoothDeltaTime):0}";
    }
}
