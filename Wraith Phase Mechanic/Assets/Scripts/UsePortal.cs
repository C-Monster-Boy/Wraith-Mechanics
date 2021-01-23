using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsePortal : MonoBehaviour
{
    public  bool usingPortal;
    public Material mat;
    public Material playerMat;
    public GameObject speedLines;
    public GameObject playerBodyGraphics;
    public GameObject voidTrail;

    private Portal portalScript;

    private int iterationStep = 3;
    private bool iterateNormally;
    private int currPosElement;

    private Animator anim;
    private VoidAudio va;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        va = GetComponent<VoidAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        if(usingPortal)
        {
            if(iterateNormally)
            {
                if(currPosElement < portalScript.posList.Count)
                {
                    transform.position = portalScript.posList[currPosElement];
                    currPosElement+= iterationStep;
                }
                else
                {
                    GetOutOfPortal();
                }
            }
            else
            {
                if (currPosElement >= 0)
                {
                    transform.position = portalScript.posList[currPosElement];
                    currPosElement-= iterationStep;
                }
                else
                {
                    GetOutOfPortal();
                }
            }
        }
    }


    public void GetIntoPortal(Portal p, int portalNumber)
    {
        EnableScripts(false);
        SetTrapKillStatus.canTrapKill = false;
        mat.SetInt("_VoidActive", 1);
        playerMat.SetInt("_IsShaderActive", 1);
        voidTrail.SetActive(true);
        speedLines.SetActive(true);
        usingPortal = true;
        anim.SetBool("Portalling", true);
        portalScript = p;
        portalScript.inUse++;
        GameObject.FindObjectOfType<PortalLimiter>().UpdateRecentlyUsedPortal(portalScript);
        va.ActivateSound();

        if (portalNumber == 1)
        {
            iterateNormally = true;
            currPosElement = 0;
        }
        else
        {
            iterateNormally = false;
            currPosElement = portalScript.posList.Count-1;
        }
    }

    void EnableScripts(bool val)
    {
        GetComponent<PlayerMovement>().enabled = val;
        GetComponent<IntoTheVoid>().enabled = val;
        GetComponent<DropPortal>().enabled = val;
    }

    public void GetOutOfPortal()
    {
        mat.SetInt("_VoidActive", 0);
        playerMat.SetInt("_IsShaderActive", 0);
        speedLines.SetActive(false);
        voidTrail.SetActive(false);
        anim.SetBool("Portalling", false);
        usingPortal = false;
        portalScript.inUse--;
        va.DeactivateSound();

        if (iterateNormally)
        {
            transform.position = portalScript.posList[portalScript.posList.Count - 1];
        }
        else
        {
            transform.position = portalScript.posList[0];
        }
        EnableScripts(true);
        SetTrapKillStatus.canTrapKill = true;
    }

}
