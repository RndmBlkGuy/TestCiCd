using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Equipment/WeaponEquipment")]
public class WeaponEquipment : Equipment
{

    [SerializeField]
    private float rateChange;
    [SerializeField]
    private float oldRate;

    public override void AddEffect(PlayerData data)
    {
        oldRate = data.bulletRate;
        data.bulletRate = rateChange; // Update the score
        Debug.Log("New Rate for bullet: " + data.bulletRate);
        DataManager.SavePlayerData(data); // Save the updated data
    }

    public override void RemoveEffect(PlayerData data)
    {
        data.bulletRate = oldRate; // Update the score
        Debug.Log("New Rate for bullet: " + data.bulletRate);
        DataManager.SavePlayerData(data); // Save the updated data
    }
}
