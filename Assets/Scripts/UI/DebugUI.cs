using TMPro;
using UnityEngine;

namespace UI
{
    public class DebugUI : MonoBehaviour
    {
        [SerializeField] private Rigidbody busRigidBody;
        [SerializeField] private TextMeshProUGUI fpsText;
        [SerializeField] private TextMeshProUGUI busSteedText;
        
        private void FixedUpdate()
        {
            busSteedText.text = $"Bus Velocity: {busRigidBody.velocity.magnitude:0.0}";
        }

        private void Update()
        {
            fpsText.text = $"FPS: {(1f / Time.smoothDeltaTime):0}";
        }
    }
}