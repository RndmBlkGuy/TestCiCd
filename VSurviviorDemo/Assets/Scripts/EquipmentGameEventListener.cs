using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EquipmentEvent : UnityEvent<Equipment> { }

public class EquipmentGameEventListener : MonoBehaviour
{
    public EquipmentGameEvent Event;
    public EquipmentEvent Response;

    private void OnEnable()
    {
        Event.OnEquipmentEventRaised += OnEventRaised;
    }

    private void OnDisable()
    {
        Event.OnEquipmentEventRaised -= OnEventRaised;
    }

    public void OnEventRaised(Equipment equipment)
    {
        Response.Invoke(equipment);
    }
}
