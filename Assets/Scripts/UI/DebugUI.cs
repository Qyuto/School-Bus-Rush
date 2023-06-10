using TMPro;
using UnityEngine;

namespace UI
{
    public class DebugUI : MonoBehaviour
    {
        [SerializeField] private Rigidbody busRigidBody;
        [SerializeField] private TextMeshProUGUI fpsText;

        private void Update()
        {
            fpsText.text = $"FPS: {(1f / Time.smoothDeltaTime):0}";
        }
    }
}