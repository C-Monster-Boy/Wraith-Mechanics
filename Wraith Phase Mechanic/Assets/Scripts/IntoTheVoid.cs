using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class IntoTheVoid : MonoBehaviour
{
    public bool inVoid;

    public Material mat;
    public Material playerMat;
    public float duration;
    public float cooldown;
    public CinemachineFreeLook cineCam;
    public float voidFOV;
    public GameObject speedLines;
    public GameObject voidTrail;
    public Image filler;

    private float currDuration;
    private float currCooldown;
    private bool canActivateVoid;
    private float baseFOV;
    private int interpolationVal;// 1=base->void  2=void->base
    private float lerper;

    private VoidAudio va;
    // Start is called before the first frame update
    void Start()
    {
        currDuration = -10;
        currCooldown = -10;
        canActivateVoid = true;
        inVoid = false;
        baseFOV = 80f;
        cineCam.m_Lens.FieldOfView = baseFOV;
        interpolationVal = 2;
        speedLines.SetActive(false);
        va = GetComponent<VoidAudio>();
        mat.SetInt("_VoidActive", 0);
        playerMat.SetInt("_IsShaderActive", 0);
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Q) && canActivateVoid)
        {
            ActivateVoid(true);
            currDuration = duration;
            canActivateVoid = false;
            //filler.fillAmount = 1f;
        }


        FOVInterpolation();

        if(currDuration>0 && currDuration != -10)
        {
            currDuration -= Time.deltaTime;
            filler.fillAmount = 1-(currDuration * 1f)/ duration;
        }
        else if(currDuration <=0 && currDuration != -10)
        {
            ActivateVoid(false);
            currCooldown = cooldown;
            currDuration = -10;
            filler.fillAmount = 1;
        }

        if(currCooldown >0 && currCooldown != 10)
        {
            currCooldown -= Time.deltaTime;
            filler.fillAmount = (currCooldown*1f) / cooldown;
        }
        else if(currCooldown <= 0 && currCooldown != -10)
        {
            canActivateVoid = true;
            currCooldown = -10;
            filler.fillAmount = 0;
        }
    }

    void FOVInterpolation()
    {
        if (interpolationVal == 1)
        {
            lerper += Time.deltaTime + 0.2f;
            lerper = Mathf.Clamp01(lerper);
            cineCam.m_Lens.FieldOfView = Mathf.Lerp(baseFOV, voidFOV, lerper);
        }
        else if (interpolationVal == 2)
        {
            lerper += Time.deltaTime;
            lerper = Mathf.Clamp01(lerper);
            cineCam.m_Lens.FieldOfView = Mathf.Lerp(voidFOV, baseFOV, lerper);
        }
    }

    void ActivateVoid(bool val)
    {
        inVoid = val;
        if(val)
        {
            mat.SetInt("_VoidActive", 1);
            playerMat.SetInt("_IsShaderActive", 1);
            lerper = 0;
            interpolationVal = 1;
            speedLines.SetActive(val);
            voidTrail.SetActive(val);
            va.ActivateSound();
        }
        else
        {
            mat.SetInt("_VoidActive", 0);
            playerMat.SetInt("_IsShaderActive", 0);
            lerper = 0;
            interpolationVal = 2;
            speedLines.SetActive(val);
            va.DeactivateSound();
            if(!GetComponent<DropPortal>().isCreatingPortal)
            {
                voidTrail.SetActive(val);
            }
            
        }
    }
}
