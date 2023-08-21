using System;
using UnityEngine;

namespace Bus
{
    public class BusDangerousLowSpeed : MonoBehaviour
    {
        [SerializeField] private BusLevelCompletion busLevelCompletion;
        [SerializeField] private float minVelocity = 5;
        [SerializeField] private Rigidbody busRigidbody;
        [SerializeField] private GameObject lowSpeedLayer;
        [SerializeField] private float maxTimeWait;
        [SerializeField] private float startDelay;

        private float _timeDelay;
        private float _timeLose;
        private bool _isLowVelocity;

        private void Awake()
        {
            busLevelCompletion.onBusLevelFailComplete.AddListener(OnLevelFailed);
            busLevelCompletion.onBusArrivedAtEnd.AddListener(OnLevelFailed);
        }

        private void OnLevelFailed()
        {
            enabled = false;
        }

        private void Update()
        {
            _timeDelay += Time.deltaTime;
            if (_timeDelay < startDelay) return;
            CheckVelocity();
            if (_isLowVelocity)
            {
                _timeLose += Time.deltaTime;
                if (_timeLose < maxTimeWait) return;
                busLevelCompletion.onBusLevelFailComplete.Invoke();
                enabled = false;
            }
        }

        private void CheckVelocity()
        {
            Vector3 velocity = busRigidbody.velocity;
            velocity.y = 0;
            if (velocity.magnitude < minVelocity)
            {
                lowSpeedLayer.SetActive(true);
                _isLowVelocity = true;
            }
            else
            {
                lowSpeedLayer.SetActive(false);
                _timeLose = 0;
                _isLowVelocity = false;
            }
        }

        private void OnDisable()
        {
            lowSpeedLayer.SetActive(false);
            _isLowVelocity = false;
        }
    }
}