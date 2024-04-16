using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentUI : MonoBehaviour
{
    public TMP_Text name;
    public TMP_Text cost;
    public string id;
    public Image sprite;
    public GameObject buyButton;
    public void Setup(Equipment equipment){

        name.text = equipment.EquipmentName;
        cost.text = "-"+equipment.EquipmentCost;
        id = equipment.EquipmentId;
        sprite.sprite = equipment.EquipmentIcon;
        buyButton.GetComponent<EquipmentBuyButton>().AddEquipment(equipment);
    }
}
