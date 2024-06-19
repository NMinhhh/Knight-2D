using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class AudioMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] protected GameObject[] musicObj;
    [SerializeField] protected GameObject[] soundObj;
    private string sound = "Sound";
    private string music = "Music";
    void Start()
    {
        if (PlayerPrefs.HasKey(sound))
        {
            float soundValue = PlayerPrefs.GetFloat(sound);
            SetVolume(sound, soundValue);
            SetButtonActive(soundObj, soundValue);
        }
        if(PlayerPrefs.HasKey(music))
        {
            float musicValue = PlayerPrefs.GetFloat(music);
            SetVolume(music, musicValue);
            SetButtonActive(musicObj, musicValue);
        }
    }

    void SetVolume(string name, float value)
    {
        audioMixer.SetFloat(name, value);
        PlayerPrefs.SetFloat(name, value);
    }

    void SetButtonActive(GameObject[] btns, float value)
    {
        if (value == 0)
        {
            SetActive(btns[0]);
            SetInActive(btns[1]);
        }
        else
        {
            SetActive(btns[1]);
            SetInActive(btns[0]);
        }
    }

    public void SetSoundVolume(float value)
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        SetVolume(sound, value);
        SetButtonActive(soundObj, value);
    }

    public void SetMusicVolume(float value)
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        SetVolume(music, value);
        SetButtonActive(musicObj, value);
    }

    void SetActive(GameObject go)
    {
        go.gameObject.SetActive(true);
    }

    void SetInActive(GameObject go)
    {
        go.gameObject.SetActive(false);
    }

    
}
