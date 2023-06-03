using Bus;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Obstacles
{
    [RequireComponent(typeof(Obstacle))]
    public class GateObstacle : MonoBehaviour
    {
        [SerializeField] private int passengerThreshold;
        [SerializeField] private float durationTime;
        [SerializeField] private Transform movableTransform;
        [SerializeField] private BusTrigger busTrigger;

        private TextMeshPro _passengerOpenThresholdText;
        private Vector3 _targetPosition;

        private void Awake()
        {
            if (passengerThreshold != 0)
            {
                _passengerOpenThresholdText = GetComponentInChildren<TextMeshPro>();
                _passengerOpenThresholdText.gameObject.SetActive(true);
                _passengerOpenThresholdText.text = passengerThreshold.ToString();
            }

            _targetPosition = movableTransform.position + Vector3.down * 20f;
            busTrigger.onTriggerEnter.AddListener(StartOpen);
        }

        private void StartOpen(GameObject bus)
        {
            if (passengerThreshold != 0)
            {
                PassengerCount passengerCount = bus.GetComponentInParent<PassengerCount>();
                if (passengerCount == null) return;
                if (passengerThreshold <= passengerCount.CurrentPassenger)
                    passengerCount.onBusLostPassenger?.Invoke(passengerThreshold);
                else return;
            }

            movableTransform.transform.DOMove(_targetPosition, durationTime);
        }
    }
}