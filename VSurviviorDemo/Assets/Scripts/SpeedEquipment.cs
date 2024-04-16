using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boots", menuName = "Equipment/SpeedEquipment")]
public class SpeedEquipment : Equipment
{
    [SerializeField]
    private int additionalSpeed;

    public override void AddEffect(PlayerData data)
    {
        data.playerSpeed += additionalSpeed; // 
        Debug.Log("New Speed for Player: " + data.playerSpeed);
        DataManager.SavePlayerData(data); // Save the updated data
    }

    public override void RemoveEffect(PlayerData data)
    {
        data.playerSpeed -= additionalSpeed; // 
        Debug.Log("New Speed for Player: " + data.playerSpeed);
        DataManager.SavePlayerData(data); // Save the updated data
    }
}

