using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bus
{
    public class BusDangerousAngel : MonoBehaviour
    {
        [SerializeField] private Transform busTransform;
        [SerializeField] private GameObject busRotationLayer;
        [SerializeField] private BusLevelCompletion levelCompletion;
        [SerializeField] private float maxAngel;
        [SerializeField] private float maxTime;
        [SerializeField] private Image busRotationImage;
        [SerializeField] private float rotationImageSpeed;
        private float _time;

        private void Awake()
        {
            levelCompletion.onBusLevelFailComplete.AddListener(OnLevelFailed);
            levelCompletion.onBusArrivedAtEnd.AddListener(OnLevelFailed);
        }

        private void OnLevelFailed()
        {
            enabled = false;
        }

        private void Update()
        {
            if (Mathf.Abs(Mathf.DeltaAngle(0, busTransform.rotation.eulerAngles.z)) >= maxAngel)
            {
                _time += Time.deltaTime;
                busRotationLayer.gameObject.SetActive(true);
                busRotationImage.transform.Rotate(0, 0, rotationImageSpeed);
            }
            else
            {
                _time = 0;
                busRotationLayer.gameObject.SetActive(false);
            }

            if (_time >= maxTime)
            {
                levelCompletion.onBusLevelFailComplete?.Invoke();
                busRotationLayer.gameObject.SetActive(false);
                enabled = false;
            }
        }

        private void OnDisable()
        {
            busRotationLayer.gameObject.SetActive(false);
        }
    }
}