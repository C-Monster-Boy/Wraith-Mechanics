using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuddingCuddingKill : MonoBehaviour
{
    public bool isKillActive;

    private void Start()
    {
        isKillActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Health>() && isKillActive && SetTrapKillStatus.canTrapKill)
        {
            Debug.Log("Killed");
            Health h = other.gameObject.GetComponent<Health>();
            h.TakeDamage(h.maxHealth);
        }
    }
}
