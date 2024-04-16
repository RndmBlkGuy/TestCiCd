using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 50f;
    public int bulletDamage = 1;

    void Awake(){
        GetBulletDamage();
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy")){
            ScoreManager.instance.AddScore(1);
            other.gameObject.GetComponent<EnemyController>().EnemyHit(bulletDamage);
            Destroy(gameObject);
        }
    }

    void GetBulletDamage(){
        bulletDamage = DataManager.LoadPlayerData().bulletDamage;
    }
}
