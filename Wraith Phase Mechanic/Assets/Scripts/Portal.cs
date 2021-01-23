using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public enum PortalState
    {
        Unspawned, OnePort, DestroyTimerSet, BothPorts
    };
    public int inUse;
    public float aliveTime;
    public GameObject player;
    public GameObject port1;
    public GameObject port2;
    [SerializeField]
    public List<Vector3> posList;
    public PortalState ps = PortalState.Unspawned;

    private float currDestroyTimer;
    private bool canDestroyPortal;
    
    // Start is called before the first frame update
    void Start()
    {
        posList = new List<Vector3>();
        currDestroyTimer = -10f;
        canDestroyPortal = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(ps == PortalState.OnePort)
        {
            /*if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                posList.Add(player.transform.position);
            }*/
            posList.Add(player.transform.position);

        }
        else if(ps == PortalState.DestroyTimerSet)
        {
            currDestroyTimer = 0;
            ps = PortalState.BothPorts;
        }

        if(currDestroyTimer >= aliveTime && currDestroyTimer != -10)
        {
            canDestroyPortal = true;
            currDestroyTimer = -10;
        }
        else if(currDestroyTimer >= 0 && currDestroyTimer != -10)
        {
            currDestroyTimer += Time.deltaTime;
        }

        if(canDestroyPortal && inUse == 0)
        {
            port1.GetComponent<Portaling>().enabled = false;
            port2.GetComponent<Portaling>().enabled = false;

            GameObject.FindObjectOfType<PortalLimiter>().RemovePortal(this, 0.8f);
            canDestroyPortal = false;
        }
    }

    public void DestroyPortal(float gap)
    {
        port1.GetComponent<PortalAudio>().DeactivateSound();
        port2.GetComponent<PortalAudio>().DeactivateSound();

        Destroy(gameObject, gap);
    }

    public void SetPortState(int n)
    {
        switch(n)
        {
            case 0:
                {
                    ps = PortalState.Unspawned;
                    break;
                }
            case 1:
                {
                    ps = PortalState.OnePort;
                    break;
                }
            case 2:
                {
                    ps = PortalState.DestroyTimerSet;
                    break;
                }
            case 3:
                {
                    ps = PortalState.BothPorts;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
