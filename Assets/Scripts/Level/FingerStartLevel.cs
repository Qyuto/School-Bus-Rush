using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Level
{
    public class FingerStartLevel : MonoBehaviour
    {
        [SerializeField] private GameObject startLayerUI;

        [SerializeField] private BusMovement busMovement;

        private void Awake()
        {
            busMovement.enabled = false;
            EnhancedTouchSupport.Enable();
            Touch.onFingerDown += EnableBusComponent;
        }

        private void EnableBusComponent(Finger obj)
        {
            busMovement.enabled = true;
            startLayerUI.SetActive(false);
        }

        private void OnDisable()
        {
            Touch.onFingerDown -= EnableBusComponent;
        }
    }
}