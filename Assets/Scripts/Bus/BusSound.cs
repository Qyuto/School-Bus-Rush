using Passenger;
using UnityEngine;

namespace Bus
{
    public class BusSound : MonoBehaviour
    {
        [SerializeField] private PassengerCount passengerCount;
        [SerializeField] private BusLevelCompletion busLevelCompletion;

        [SerializeField] private AudioClip pickUpClip;
        [SerializeField] private AudioClip explosionClip;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            busLevelCompletion.onBusArrivedAtEnd.AddListener(OnBusArrived);
            passengerCount.onBusCollectPassenger.AddListener(StartPickUpSound);
            passengerCount.onBusLostPassenger.AddListener(StartExplosionSound);
        }

        private void OnDisable()
        {
            busLevelCompletion.onBusArrivedAtEnd.RemoveListener(OnBusArrived);
            busLevelCompletion.onBusArrivedAtEnd.RemoveListener(OnBusArrived);
            passengerCount.onBusCollectPassenger.RemoveListener(StartPickUpSound);
        }

        public void SetSoundVolume(float value)
        {
            _audioSource.volume = value;
        }

        private void OnBusArrived()
        {
            enabled = false;
        }

        private void StartPickUpSound(IPassengerModifier arg0)
        {
            _audioSource.clip = pickUpClip;
            _audioSource.Play();
        }

        private void StartExplosionSound(IPassengerModifier arg0)
        {
            if (arg0.GetModifierType() == "AgentCoordinator") return;
            _audioSource.clip = explosionClip;
            _audioSource.Play();
        }
    }
}