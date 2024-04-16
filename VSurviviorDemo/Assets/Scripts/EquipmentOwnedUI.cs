using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentOwnedUI : MonoBehaviour
{

    public Image sprite;
    public GameObject equipButton;
    public Equipment currentEquipment;
    public void Setup(Equipment equipment){

        currentEquipment = equipment;
        sprite.sprite = currentEquipment.EquipmentIcon;
    }
}
