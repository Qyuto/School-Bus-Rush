using Bus;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Passenger
{
    public class PassengerGroup : MonoBehaviour, IInteractable
    {
        [SerializeField] private float jumpOffset = 2f;
        [SerializeField] private int pickupCount;
        [SerializeField] private Transform[] peopleTransforms;
        [SerializeField] private TextMeshPro textPeopleCount;
        public int PeopleCount => pickupCount;

        private void Awake()
        {
            textPeopleCount.text = pickupCount.ToString();
        }

        public void Select(IBusInteractor interact)
        {
            PassengerCount passengerCount = interact.GetPassengerCountComponent();
            passengerCount.onBusCollectPassenger?.Invoke(pickupCount);

            Transform interactTransform = interact.GetTransform();
            GetComponent<Collider>().enabled = false;
            transform.DOJump(interactTransform.position + interactTransform.forward * jumpOffset, 5, 1, 0.9f)
                .onComplete += OnDotComplete;
            transform.DOScale(0.2f, 0.9f);
        }

        private void OnDotComplete()
        {
            Destroy(gameObject);
        }
    }
}