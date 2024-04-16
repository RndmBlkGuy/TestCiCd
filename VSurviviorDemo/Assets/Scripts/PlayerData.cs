using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public int playerScore;
    public float playerSpeed;
    public float bulletRate;
    public int bulletDamage;
    public List<string> ownedItemIDs = new List<string>(); // Assuming each equipment has a unique ID
    public List<string> equippedItemIDs = new List<string>();

}
