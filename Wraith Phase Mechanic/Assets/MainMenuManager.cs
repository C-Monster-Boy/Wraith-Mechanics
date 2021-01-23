using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject keyBindings;
    public GameObject instructions;
    public GameObject settings;
    public GameObject about;
    public GameObject quit;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMainMenu();
        }
    }

    public void SetKeyBindings(bool val)
    {
        keyBindings.SetActive(val);
    }

    public void SetQuit(bool val)
    {
        quit.SetActive(val);
    }

    public void SetAbout(bool val)
    {
        about.SetActive(val);
    }

    public void SetInstructions(bool val)
    {
        instructions.SetActive(val);
    }

    public void SetSettings(bool val)
    {
        settings.SetActive(val);
    }

    void GoToMainMenu()
    {
        SetKeyBindings(false);
        SetInstructions(false);
        SetSettings(false);
        SetAbout(false);
        SetQuit(false);
    }
}
