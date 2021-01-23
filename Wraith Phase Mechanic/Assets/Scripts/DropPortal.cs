using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropPortal : MonoBehaviour
{
    public  bool isCreatingPortal;

    public GameObject electricity;
    public GameObject portal;
    public GameObject portalGraphic;
    public float dropPortalCooldown;
    public float portalDestroyTime;
    public float maxPortalLength;
    public float minPortalDist;
    public GameObject portalSpwanPos;
    public GameObject voidTrial;
    public Image filler;
    [SerializeField]
    private float currPortalLength;
    private float currDropPortalCooldown;
    private bool canDropPortal;
    
    private GameObject droppedPortal;

    // Start is called before the first frame update
    void Start()
    {
        currPortalLength = -10;
        canDropPortal = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && !isCreatingPortal && !GetComponent<IntoTheVoid>().inVoid && canDropPortal)
        {
            StartPortal();
            canDropPortal = false;
            //filler.fillAmount = 1;
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1) && isCreatingPortal)
        {
            EndPortal();
        }

        if(currPortalLength >= maxPortalLength && currPortalLength != -10)
        {
            currPortalLength = -10;
            if(isCreatingPortal)
            {
                EndPortal();
            }
            
        }
        else if(currPortalLength >= 0 && currPortalLength != -10)
        {
            if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                currPortalLength += Time.deltaTime;
            }
            filler.fillAmount = currPortalLength / (maxPortalLength * 1f);
        }

        if(currDropPortalCooldown > 0 && currDropPortalCooldown != -10)
        {
            currDropPortalCooldown -= Time.deltaTime;
            filler.fillAmount = (currDropPortalCooldown * 1f) / dropPortalCooldown;
        }
        else if(currDropPortalCooldown <= 0 && currDropPortalCooldown != -10)
        {
            canDropPortal = true;
            filler.fillAmount = 0;
            currDropPortalCooldown = -10;
        }
    }

    void StartPortal()
    {
        voidTrial.SetActive(true);
        electricity.SetActive(true);

        droppedPortal = Instantiate(portal, transform.position, Quaternion.identity) as GameObject;
        Portal portalScript = droppedPortal.GetComponent<Portal>();

        portalScript.player = gameObject;

        GameObject p_grphic_1 = Instantiate(portalGraphic, portalSpwanPos.transform.position, Quaternion.identity, droppedPortal.transform) as GameObject;
        p_grphic_1.transform.eulerAngles = new Vector3(p_grphic_1.transform.eulerAngles.x, portalSpwanPos.transform.eulerAngles.y+90f, 90f);
        p_grphic_1.transform.position = portalSpwanPos.transform.position; //new Vector3(p_grphic_1.transform.position.x + 1.5f, 1f, p_grphic_1.transform.position.z + 1.5f);

        portalScript.port1 = p_grphic_1;
        portalScript.SetPortState(1);

        isCreatingPortal = true;
        currPortalLength = 0;
        p_grphic_1.GetComponent<PortalAudio>().ActivateSound();
    }

    void EndPortal()
    {
        voidTrial.SetActive(false);
        electricity.SetActive(false);

        Portal portalScript = droppedPortal.GetComponent<Portal>();

        GameObject p_grphic_2 = Instantiate(portalGraphic, portalSpwanPos.transform.position, Quaternion.identity, droppedPortal.transform) as GameObject;
        p_grphic_2.transform.eulerAngles = new Vector3(p_grphic_2.transform.eulerAngles.x, portalSpwanPos.transform.eulerAngles.y + 90f, 90f);
        p_grphic_2.transform.position = portalSpwanPos.transform.position;//new Vector3(p_grphic_2.transform.position.x + 1.5f, 1f, p_grphic_2.transform.position.z);

        portalScript.port2 = p_grphic_2;
        portalScript.SetPortState(2);

        isCreatingPortal = false;

        float prevPortLength = currPortalLength;
        currPortalLength = -10;

        GameObject.FindObjectOfType<PortalLimiter>().AddPortalToList(portalScript);

        p_grphic_2.GetComponent<PortalAudio>().ActivateSound();
        currDropPortalCooldown = dropPortalCooldown;
        filler.fillAmount = 1;

        try
        {
            CheckVeryClosePortals(portalScript, prevPortLength);
        }
        catch
        {

        }
        
    }

    void CheckVeryClosePortals(Portal p, float portLength)
    {
        float xDist = Mathf.Abs(p.posList[0].x - p.posList[p.posList.Count - 1].x);
        float zDist = Mathf.Abs(p.posList[0].z - p.posList[p.posList.Count - 1].z);

        if(xDist < minPortalDist && zDist < minPortalDist && portLength < 1f)
        {
            GameObject.FindObjectOfType<PortalLimiter>().RemovePortal(p, 0.5f);
        }

    }

}
