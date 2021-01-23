using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTrapKillStatus : MonoBehaviour
{
    public static bool canTrapKill;

    private HuddingCuddingKill hc;
    private AudioSource audioSource;
    public bool killActive;
    // Start is called before the first frame update
    void Start()
    {
        canTrapKill = true;
        hc = transform.GetChild(0).gameObject.GetComponent<HuddingCuddingKill>();
        audioSource = GetComponent<AudioSource>();
        killActive = false;
    }

    public void SetKillTrue()
    {
        hc.isKillActive = true;

    }

    public void SetKillFalse()
    {
        hc.isKillActive = false;
    }

    public void PlayThud()
    {
        audioSource.Play();
    }



}
