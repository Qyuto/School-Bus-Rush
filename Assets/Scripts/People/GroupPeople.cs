using Bus;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GroupPeople : MonoBehaviour, IInteractable
{
    [SerializeField] private float jumpOffset = 2f;
    [SerializeField] private int peopleCount;
    [SerializeField] private TextMeshPro textPeopleCount;
    public int PeopleCount => peopleCount;

    private void Awake()
    {
        textPeopleCount.text = peopleCount.ToString();
    }

    public void Select(IBusInteractor interact)
    {
        PassengerCount passengerCount = interact.GetPassengerCountComponent();
        passengerCount.onBusCollectPassenger?.Invoke(peopleCount);

        Transform interactTransform = interact.GetTransform();
        GetComponent<Collider>().enabled = false;
        transform.DOJump(interactTransform.position + interactTransform.forward * jumpOffset, 5, 1, 0.9f).onComplete +=
            OnDotComplete;
        transform.DOScale(0.2f, 0.9f);
    }

    private void OnDotComplete()
    {
        Destroy(gameObject);
    }
}