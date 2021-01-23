using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensSliderHandler : MonoBehaviour
{
    public Slider xSlider;
    public Slider ySlider;
    public Text xVal;
    public Text yVal;

    // Start is called before the first frame update
    void Start()
    {
        float x = PlayerPrefs.GetFloat("XSens", 500f);
        float y = PlayerPrefs.GetFloat("YSens", 500f);

        xSlider.value = x;
        ySlider.value = y;
        xVal.text = x + "";
        yVal.text = y + "";
    }

    // Update is called once per frame
    void Update()
    {
        xVal.text = xSlider.value + "";
        yVal.text = ySlider.value + "";

        PlayerPrefs.SetFloat("XSens", xSlider.value);
        PlayerPrefs.SetFloat("YSens", ySlider.value);
    }
}
