using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;
public class DamageOnImpact : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>() && !collision.gameObject.GetComponent<IntoTheVoid>().inVoid && !collision.gameObject.GetComponent<UsePortal>().usingPortal)
        {
            print("Damage");
            collision.gameObject.GetComponent<Health>().TakeDamage(50);
        }

        Destroy(gameObject);
    }
}
