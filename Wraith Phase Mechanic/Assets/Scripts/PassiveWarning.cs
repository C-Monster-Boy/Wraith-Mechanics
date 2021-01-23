using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveWarning : MonoBehaviour
{
    public AudioSource warningAudio;
    public float timeBtnWarnings;

    private float currWarningTime;

    // Start is called before the first frame update
    void Start()
    {
        currWarningTime = -10;
    }

    // Update is called once per frame
    void Update()
    {
        if(currWarningTime >= timeBtnWarnings && currWarningTime != -10)
        {
            currWarningTime = -10f;
        }
        else if(currWarningTime >= 0 && currWarningTime != -10)
        {
            currWarningTime += Time.deltaTime;
        }
    }

    public void Warn()
    {
        if(!warningAudio.isPlaying && currWarningTime == -10)
        {
            warningAudio.Play();
            currWarningTime = 0;
        }
    }
}
