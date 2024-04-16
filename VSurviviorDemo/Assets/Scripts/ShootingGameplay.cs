using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGameplay : MonoBehaviour
{
    public Bullet bullet;
    public GameObject bulletStartPos;
    public bool isRapidFire;
    public float bulletLastFired;
    public float bulletRate;

    public AudioManager audioManager;

    private void Start() {   
        GetBulletRate();
        audioManager = GetComponentInParent<AudioManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1")){
            float timeSinceLastFired = Time.time - bulletLastFired;
            if(timeSinceLastFired >= bulletRate){
                ShootProjectile();
                bulletLastFired = Time.time;
                
            }
            
        }
    }

    void ShootProjectile()
    {
        audioManager.PlayShootingAudio();
        GameObject projectile = Instantiate(bullet.gameObject, transform.position, transform.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.velocity = transform.forward * bullet.bulletSpeed;
    }

    bool isRapidFiring(){
        return isRapidFire = Input.GetButton("Fire1");
    } 

    void GetBulletRate(){
        bulletRate = DataManager.LoadPlayerData().bulletRate;
    }
}
