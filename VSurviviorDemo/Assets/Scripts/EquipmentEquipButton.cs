using UnityEngine;

public class EquipmentEquipButton : MonoBehaviour
{
    public Equipment equipment;
    public EquipmentGameEvent EquipEvent;
    public EquipmentGameEvent UnequippedEvent;

    public void AddEquipment(Equipment newEquipment){
        if (equipment == null){
            equipment = newEquipment;
        }
    }
    public void OnEquipButtonPressed()
    {
        EquipEvent.Raise(equipment);
    }
    public void OnUnEquippedButton(){
        UnequippedEvent.Raise(equipment);
    }
}
