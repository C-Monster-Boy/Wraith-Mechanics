using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapArea : MonoBehaviour
{

    public float amplitudeVal;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Health>())
        {
            other.gameObject.GetComponent<Health>().SetRigNoise(amplitudeVal);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Health>())
        {
            other.gameObject.GetComponent<Health>().SetRigNoise(0);
        }
    }
}
