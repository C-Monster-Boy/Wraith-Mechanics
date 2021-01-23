using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsHandler : MonoBehaviour
{
    public Sprite[] images;
    public Image content;
    public GameObject chevRight;
    public GameObject chevLeft;

    private int imageToShow;
    private int currImage;

    // Start is called before the first frame update
    void Start()
    {
        currImage = -1;
        imageToShow = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(currImage != imageToShow)
        {
            content.sprite = images[imageToShow];
            currImage = imageToShow;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PrevImage();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextImage();
        }
    }

    public void NextImage()
    {
        if(currImage < images.Length-1)
        {
            if(currImage == images.Length-2)
            {
                chevRight.SetActive(false);
            }

            if(!chevLeft.activeSelf)
            {
                chevLeft.SetActive(true);
            }

            imageToShow++;
        }
    }

    public void PrevImage()
    {
        if (currImage > 0)
        {
            if (currImage == 1)
            {
                chevLeft.SetActive(false);
            }

            if (!chevRight.activeSelf)
            {
                chevRight.SetActive(true);
            }

            imageToShow--;
        }
    }
}
