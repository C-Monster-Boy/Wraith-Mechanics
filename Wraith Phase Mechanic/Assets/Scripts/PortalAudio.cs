using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] clips; //0=start 1=loop 2=finish

    // Start is called before the first frame update

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ActivateSound()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clips[0];
        audioSource.loop = false;
        audioSource.Play();
        StartCoroutine(PlayLoopSound(0.5f));

    }

    public void DeactivateSound()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.clip = clips[2];
        audioSource.Play();
        
    }

    private IEnumerator PlayLoopSound(float time)
    {
        audioSource = GetComponent<AudioSource>();

        yield return new WaitForSeconds(time);

        audioSource.clip = clips[1];
        audioSource.loop = true;
        audioSource.Play();
    }
}
