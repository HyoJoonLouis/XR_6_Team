using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class audioMixer : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider MasterSlider;
    public Slider BGMSlider;
    public Slider SFXSlider;

    public Sprite Ok;
    public Sprite No;

    public Image Full;
    public Image Window;

    public Sprite Mute;
    public Sprite NotMute;

    public Image MasterImage;
    public Image BGMImage;
    public Image SFXImage;

    public void MasterControl()
    {
        float sound = MasterSlider.value;

        if (sound == -40f)
        {
            MasterImage.sprite = Mute;
            masterMixer.SetFloat("Master", -80);
        }
        else
        {

            MasterImage.sprite = NotMute;
            masterMixer.SetFloat("Master", sound);

        }
    }
    public void BGMControl()
    {
        float sound = MasterSlider.value;

        if (sound == -40f)
        {
            BGMImage.sprite = Mute;
            masterMixer.SetFloat("BGM", -80);
        }
        else
        {
            BGMImage.sprite = NotMute;
            masterMixer.SetFloat("BGM", sound);
        }
    }
    public void SFXControl()
    {
        float sound = MasterSlider.value;

        if (sound == -40f)
        {
            SFXImage.sprite = Mute;
            masterMixer.SetFloat("SFX", -80);
        }
        else
        {
            SFXImage.sprite = NotMute;
            masterMixer.SetFloat("SFX", sound);
        }
    }

    public void SetFullScreen()
    {
        Full.sprite = Ok;
        Window.sprite = No;
        Screen.SetResolution(1920, 1080, true);
    }

    public void SetWindowScreen()
    {
        Full.sprite = No;
        Window.sprite = Ok;
        Screen.SetResolution(1920, 1080, false);
    }
}
