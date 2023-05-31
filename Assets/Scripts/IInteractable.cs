using UnityEngine;

public interface IInteractable
{
    public InteractableType GetInteractableType();
    public void Select(GameObject interactor);
}

public enum InteractableType
{
    Passenger = 0,
    Obstacle,
    Modifier,
    LoseObject
}