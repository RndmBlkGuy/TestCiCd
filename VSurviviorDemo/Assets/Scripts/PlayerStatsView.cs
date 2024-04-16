using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsView : MonoBehaviour
{
    // Start is called before the first frame update
    public int playerScore;
    public float playerSpeed;
    public float bulletRate;
    public int bulletDamage;
    void awake()
    {
        PlayerData data = DataManager.LoadPlayerData();
        playerScore = data.playerScore;
        bulletRate = data.bulletRate;
        bulletDamage = data.bulletDamage;
        playerSpeed = data.playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
