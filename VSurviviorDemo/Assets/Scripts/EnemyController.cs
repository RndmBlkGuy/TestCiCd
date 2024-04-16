using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform target; // Target to seek
    public NavMeshAgent agent;
    public int enemyHealth;
    public int enemyScore;
    public AudioManager audioManager;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target =  GameObject.FindGameObjectWithTag("Player").transform;
        audioManager = GetComponent<AudioManager>();
    }
 
    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    public void EnemyHit(int damage){
        enemyHealth-=damage;

        if(enemyHealth <= 0){
            EnemyDeath();
        }
    }

    void EnemyDeath(){
        audioManager.PlayHurtingAudio();
        ScoreManager.instance.AddScore(enemyScore);
        Debug.Log("Enemy death gives score :" + enemyScore);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Has collided with" + other.gameObject.tag);
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerMovement>().PlayerHit();
        }
    }  
}
