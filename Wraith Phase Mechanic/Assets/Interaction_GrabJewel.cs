using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction_GrabJewel : MonoBehaviour, IInteractable
{
    public Animator anim;
    public GameObject jewelActive;
    public AudioSource audioSource;

    void Start()
    {
        //interactionPrompt = GameObject.Find("InteractionPrompt").GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Health>())
        {
           
            DoOnInteract();
           
        }
       
    }

    public void DoOnInteract()
    {
        if (CheckDistanceFromPlayer())
        {
            // Add Jewel to inventory
            jewelActive.SetActive(true);
            GetComponent<AudioSource>().Play();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            Destroy(gameObject, 2f);
            anim.SetTrigger("Retract");
            audioSource.Play();
        }
    }

    public bool CheckDistanceFromPlayer()
    {
        float threshold = 7f;
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        float xDist = Mathf.Abs(p.transform.position.x - transform.position.x);
        float zDist = Mathf.Abs(p.transform.position.z - transform.position.z);

        return (xDist < threshold) && (zDist < threshold);
    }

}
