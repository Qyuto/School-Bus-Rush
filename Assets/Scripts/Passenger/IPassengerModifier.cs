namespace Passenger
{
    public interface IPassengerModifier
    {
        public int GetPassengerCount();
        public string GetModifierType();
    }
}