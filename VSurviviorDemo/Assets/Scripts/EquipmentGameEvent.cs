using UnityEngine;

[CreateAssetMenu(fileName = "New EquipmentGameEvent", menuName = "Game Event/Equipment Game Event")]
public class EquipmentGameEvent : ScriptableObject
{
    public delegate void EquipmentEvent(Equipment equipment);
    public event EquipmentEvent OnEquipmentEventRaised;

    public void Raise(Equipment equipment)
    {
        OnEquipmentEventRaised?.Invoke(equipment);
    }
}
