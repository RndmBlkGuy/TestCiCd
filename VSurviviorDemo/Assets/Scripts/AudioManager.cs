using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shootingSFX;
    public AudioClip hurtingSFX;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (audioSource != null){
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayShootingAudio(){
        if(shootingSFX != null){
            audioSource.PlayOneShot(shootingSFX);
        }
    }

    public void PlayHurtingAudio(){
        if(hurtingSFX != null){
            audioSource.PlayOneShot(hurtingSFX);
        }
    }
}
