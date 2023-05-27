public interface IInteractable
{
    public InteractableType GetInteractableType();
    public void Select();
}

public enum InteractableType
{
    Passenger = 0,
    Obstacle,
    Boost
}