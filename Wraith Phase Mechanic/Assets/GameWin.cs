using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameWin : MonoBehaviour
{
    public GameObject jewelStatus;
    public Animator anim;
    public GameObject fakeJewel;
    public GameObject particles;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Health>()
             && jewelStatus.activeSelf
            )
        {
            //Win
            StartCoroutine(LoadGivenScene("GameWin", 10.5f));
            EnableScripts(false);
            fakeJewel.SetActive(true);
            particles.SetActive(false);
            anim.SetTrigger("Victory");
           
            
        }
    }

    IEnumerator LoadGivenScene(string name, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(name);
    }

    void EnableScripts(bool val)
    {
        anim.gameObject.GetComponent<PlayerMovement>().enabled = val;
        //GetComponent<IntoTheVoid>().enabled = val;
        //GetComponent<DropPortal>().enabled = val;
    }
}
