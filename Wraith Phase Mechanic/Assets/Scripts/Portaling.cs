using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portaling : MonoBehaviour
{
    Portal portalScript;


    // Start is called before the first frame update
    void Start()
    {
        portalScript = gameObject.transform.parent.gameObject.GetComponent<Portal>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(CanUsePortalCheck(other))
        {
            int portalNo = (portalScript.port1 == gameObject) ? 1 : 2;


            other.gameObject.GetComponent<UsePortal>().GetIntoPortal(portalScript, portalNo);
        }
    }

    bool CanUsePortalCheck(Collider other)
    {
        return other.gameObject.layer == 8 &&
            portalScript.ps == Portal.PortalState.BothPorts &&
            !other.gameObject.GetComponent<UsePortal>().usingPortal &&
            !other.gameObject.GetComponent<DropPortal>().isCreatingPortal &&
            !other.gameObject.GetComponent<IntoTheVoid>().inVoid;

    }
}
