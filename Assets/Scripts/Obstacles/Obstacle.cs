using UnityEngine;

public class Obstacle : MonoBehaviour, IInteractable
{
    [SerializeField] private int dividePassenger;

    public int DividePassenger => dividePassenger;
    public InteractableType GetInteractableType() => InteractableType.Obstacle;
    public void Select()
    {
        Destroy(gameObject);
    }
}