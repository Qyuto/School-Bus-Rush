using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Bus
{
    public class PassengerCount : MonoBehaviour
    {
        [SerializeField] private int currentPassenger;
        [SerializeField] private TextMeshPro busPassengerCountText;

        public UnityEvent<int> onBusCollectPassenger;
        public UnityEvent<int> onBusLostPassenger;

        private Animator _busTextAnimator;
        public int CurrentPassenger => currentPassenger;

        private void Awake()
        {
            onBusCollectPassenger.AddListener(OnCollectPeople);
            onBusLostPassenger.AddListener(OnLostPeople);
            _busTextAnimator = busPassengerCountText.GetComponent<Animator>();
        }

        private void OnLostPeople(int removePeopleSize)
        {
            currentPassenger -= removePeopleSize;
            busPassengerCountText.text = currentPassenger.ToString();
            if (currentPassenger <= 0) Debug.Log($"BUS: Dont have passengers");
            _busTextAnimator.SetTrigger("Bounce");
        }

        private void OnCollectPeople(int addPeopleSize)
        {
            currentPassenger += addPeopleSize;
            busPassengerCountText.text = currentPassenger.ToString();
            
            _busTextAnimator.SetTrigger("Bounce");
        }
    }
}