using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    public FatassData data;
    public Slider Slider;
    public TMP_Text text;
    public bool whatSetting;
    float currentValue;

    // Start is called before the first frame update
    void Start()
    {
        //decide whether the slider is for SFX or music.
        if (whatSetting == false)
        {
            currentValue = data.SFXvolume;
        }
        if (whatSetting == true)
        {
            currentValue = data.Musicvolume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //display current volume
        currentValue = Slider.value;
        text.text = currentValue.ToString();
    }
}
