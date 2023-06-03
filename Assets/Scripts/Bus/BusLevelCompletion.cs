using System;
using UnityEngine;
using UnityEngine.Events;

namespace Bus
{
    public class BusLevelCompletion : MonoBehaviour
    {
        public UnityEvent onBusArrivedAtEnd;
        public UnityEvent onBusLevelComplete;
        public UnityEvent onBusLevelFailComplete;
        public bool isArrived { get; private set; }

        private void Awake()
        {
            onBusArrivedAtEnd.AddListener(() => isArrived = true);
        }

        private void OnDestroy()
        {
            onBusArrivedAtEnd.RemoveAllListeners();
            onBusLevelComplete.RemoveAllListeners();
            onBusLevelFailComplete.RemoveAllListeners();
        }
    }
}