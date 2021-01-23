using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDeath : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Health>())
        {
            Health h = collision.gameObject.GetComponent<Health>();
            h.PlayLavaBurn();
            h.TakeDamage(100);
        }
    }
}
