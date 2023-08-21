using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class TapStartLevel : MonoBehaviour
    {
        [SerializeField] private GameObject startLayerUI;
        [SerializeField] private Button buttonStart;
        [SerializeField] private MonoBehaviour[] enabledObjects;

        private void Awake()
        {
            buttonStart.onClick.AddListener(EnableBusComponent);
            foreach (var enabledObject in enabledObjects)
                enabledObject.enabled = false;
        }

        private void EnableBusComponent()
        {
            foreach (var enabledObject in enabledObjects)
                enabledObject.enabled = true;
            startLayerUI.SetActive(false);
        }
    }
}