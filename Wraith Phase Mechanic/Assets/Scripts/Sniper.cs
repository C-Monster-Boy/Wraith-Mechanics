using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    public Transform rayPoint;
    public float distanceOfShot;
    public float coolDownTime;
    public bool playerFound;

    private LineRenderer lr;
    private Quaternion defaultRot;
    
    private GameObject player;
    private GameObject hitObject;
    private float currCooldownTime;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        currCooldownTime = -10;
        defaultRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerFound)
        {
            Vector3 lookAtVector = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z);
            transform.LookAt(lookAtVector);
        }

        RaycastHit hit;
        Physics.Raycast(rayPoint.position, rayPoint.forward * distanceOfShot, out hit);

        hitObject = hit.collider.gameObject;

        lr.SetPosition(0, rayPoint.position);
        lr.SetPosition(1, hit.point);
        //Debug.Log(hit.collider.gameObject);
        if(hitObject.GetComponent<PassiveWarning>())
        {
            hitObject.GetComponent<PassiveWarning>().Warn();
        }

        if(hitObject == player && !player.GetComponent<IntoTheVoid>().inVoid && !player.GetComponent<UsePortal>().usingPortal && player.GetComponent<Health>().currHealth > 0 )
        {
            playerFound = true;
            currCooldownTime = -10;
        }
        else if(currCooldownTime == -10 && playerFound)
        {
            playerFound = false;
            currCooldownTime = 0;
        }

        if (currCooldownTime > coolDownTime && currCooldownTime != -10)
        {
            transform.rotation = defaultRot;
            currCooldownTime = -10;
            
        }
        else if(currCooldownTime >= 0 && currCooldownTime != -10)
        {
            float slerp = currCooldownTime / coolDownTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, defaultRot, slerp);
            currCooldownTime += Time.deltaTime;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayPoint.position, rayPoint.forward*distanceOfShot);
    }
}
