using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    void Start()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            LoadMusicVolume();
        }
        if (PlayerPrefs.HasKey("Sound"))
        {
            LoadSoundVolume();
        }
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("Music",Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("Music", value);
    }

    void LoadMusicVolume()
    {
        float value = PlayerPrefs.GetFloat("Music");
        audioMixer.SetFloat("Music", Mathf.Log10(value) * 20);
        musicSlider.value = value;
    }

    public void SetSoundVolume(float value)
    {
        audioMixer.SetFloat("Sound", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("Sound", value);
    }

    void LoadSoundVolume()
    {
        float value = PlayerPrefs.GetFloat("Sound");
        audioMixer.SetFloat("Sound", Mathf.Log10(value) * 20);
        soundSlider.value = value;
    }
}
