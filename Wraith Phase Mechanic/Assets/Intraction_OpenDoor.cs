using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intraction_OpenDoor : MonoBehaviour, IInteractable
{
    private Animator anim;
    public Text interactionPrompt;
    public string prompt;

    private bool interacted;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        interacted = false;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        //interactionPrompt = GameObject.Find("InteractionPrompt").GetComponent<Text>();
    }

    private void OnMouseOver()
    {
        if(CheckDistanceFromPlayer() && !interacted)
        {
            interactionPrompt.text = prompt;
            interactionPrompt.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                DoOnInteract();
                interacted = true;
                audioSource.Play();
            }
        }
    }

    private void OnMouseExit()
    {
        interactionPrompt.gameObject.SetActive(false);
    }

    public void DoOnInteract()
    {
        anim.SetTrigger("OpenDoor");
        
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
