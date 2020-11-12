using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public string parameterName;

    // Start is called before the first frame update
    void Start()
    {
        var slider = GetComponent<Slider>();
        if (AudioMixer.GetFloat(parameterName, out var volume))
            slider.value = volume;
        slider.onValueChanged.AddListener(OnSliderChange);
    }


    void OnSliderChange(float value)
    {
        if(value == GetComponent<Slider>().minValue)
            AudioMixer.SetFloat(parameterName, 0f);
        else
            AudioMixer.SetFloat(parameterName, value);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
