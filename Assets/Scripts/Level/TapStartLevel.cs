using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class TapStartLevel : MonoBehaviour
    {
        [SerializeField] private GameObject startLayerUI;
        [SerializeField] private Button buttonStart;
        [SerializeField] private BusMovement busMovement;

        private void Awake()
        {
            buttonStart.onClick.AddListener(EnableBusComponent);
            busMovement.enabled = false;
        }

        private void EnableBusComponent()
        {
            busMovement.enabled = true;
            startLayerUI.SetActive(false);
        }
    }
}