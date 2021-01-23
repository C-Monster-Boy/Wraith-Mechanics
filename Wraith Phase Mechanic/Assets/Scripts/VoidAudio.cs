using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateSound()
    {
        audioSource.clip = clips[0];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void DeactivateSound()
    {
        audioSource.loop = false;
        audioSource.clip = clips[1];
        audioSource.Play();
    }
}
