using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//All portal destruction triggered through this script

public class PortalLimiter : MonoBehaviour
{
    public List<Portal> portals;
    public int maxPortalsActive;
    // Start is called before the first frame update
    void Start()
    {
        portals = new List<Portal>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddPortalToList(Portal p)
    {
        if(portals.Count >= maxPortalsActive)
        {
            Portal temp = portals[0];
            portals.RemoveAt(0);
            portals.Add(p);
            temp.DestroyPortal(0.8f);
        }
        else
        {
            portals.Add(p);
        }
        
    }

    public void UpdateRecentlyUsedPortal(Portal p)
    {
        int index = -1;
        for(int i=0; i<portals.Count; i++)
        {
            if(portals[i] == p)
            {
                index = i;
                break;
            }
        }

        try
        {
            portals.RemoveAt(index);
            portals.Add(p);
        }
        catch(Exception e)
        {

        }
       
    }

    public void RemovePortal(Portal p, float gap)
    {
        int index = -1;
        for (int i = 0; i < portals.Count; i++)
        {
            if (portals[i] == p)
            {
                index = i;
                break;
            }
        }

        Portal temp = portals[index];
        portals.RemoveAt(index);
        temp.DestroyPortal(gap);


    }
}
