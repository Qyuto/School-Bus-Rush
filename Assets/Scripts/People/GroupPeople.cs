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

    public InteractableType GetInteractableType() => InteractableType.Passenger;

    public void Select(GameObject interact)
    {
        GetComponent<Collider>().enabled = false;
        transform.DOJump(interact.transform.position + interact.transform.forward * jumpOffset, 5, 1, 0.9f).onComplete += OnDotComplete;
        transform.DOScale(0.2f, 0.9f);
        // transform.DOShakeScale(1f, 0.5f, 3);
    }

    private void OnDotComplete()
    {
        Destroy(gameObject);
    }
}