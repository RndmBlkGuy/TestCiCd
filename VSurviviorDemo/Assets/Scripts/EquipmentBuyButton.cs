using UnityEngine;

public class EquipmentBuyButton : MonoBehaviour
{
    public Equipment equipment;
    public EquipmentGameEvent purchaseEvent;

    public void AddEquipment(Equipment newEquipment){
        if (equipment == null){
            equipment = newEquipment;
        }
    }
    public void OnBuyButtonPressed()
    {
        purchaseEvent.Raise(equipment);
    }
}
