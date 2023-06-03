using Bus;
using UnityEngine;

public interface IBusInteractor
{
    public PassengerCount GetPassengerCountComponent();
    public ModifierReceiver GetModifierReceiver();
    public BusLevelCompletion GetBusLevelCompletion();
    public Transform GetTransform();
}