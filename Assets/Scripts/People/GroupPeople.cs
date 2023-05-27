using UnityEngine;
using TMPro;

public class GroupPeople : MonoBehaviour, IInteractable
{
    [SerializeField] private int peopleCount;
    [SerializeField] private TextMeshPro textPeopleCount; 
    public int PeopleCount => peopleCount;

    private void Awake()
    {
        textPeopleCount.text = peopleCount.ToString();
    }

    public InteractableType GetInteractableType() => InteractableType.Passenger;
    public void Select()
    {
        Destroy(gameObject);
    }
}