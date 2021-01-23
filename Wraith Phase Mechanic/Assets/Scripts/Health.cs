using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currHealth;
    public Material mat;
    public Material playerMat;
    public Image healthFill;
    public CinemachineFreeLook cine;
    public float shakeDuration;
    public float shakeAmplitude;
    public AudioSource deathSound;
    public AudioSource lavaBurn;
    
    private float currShakeDurtion;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        SetRigNoise(0);
        currShakeDurtion = -10;

        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        healthFill.fillAmount = currHealth /( maxHealth * 1f);

        if(currShakeDurtion >= shakeDuration && currShakeDurtion != -10)
        {
            SetRigNoise(0);
            currShakeDurtion = -10;
        }
        else if(currShakeDurtion >= 0 && currShakeDurtion != -10)
        {
            currShakeDurtion += Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(LoadGivenScene("MainMenu", 0f));
        }
        
    }

    public void TakeDamage(int val)
    {
        CamShake();

        if(currHealth > val)
        {
            currHealth -= val;
        }
        else if(currHealth > 0)
        {
            currHealth = 0;
            Die();
        }
    }

    public void SetRigNoise(float val)
    {
        cine.GetRig(0).GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = val;
        cine.GetRig(1).GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = val;
        cine.GetRig(2).GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = val;
    }

   void CamShake()
    {
        SetRigNoise(shakeAmplitude);
        currShakeDurtion = 0;
    }

    public void Die()
    {
        anim.SetTrigger("Death");
        mat.SetInt("_VoidActive", 0);
        playerMat.SetInt("_IsShaderActive", 0);
        EnableScripts(false);
        deathSound.Play();
        StartCoroutine(LoadGivenScene("GameLose", 3f));

    }

    IEnumerator LoadGivenScene(string name, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(name);
    }

    public void PlayLavaBurn()
    {
        lavaBurn.Play();
    }

    void EnableScripts(bool val)
    {
        GetComponent<PlayerMovement>().enabled = val;
        GetComponent<IntoTheVoid>().enabled = val;
        GetComponent<DropPortal>().enabled = val;
    }
}
