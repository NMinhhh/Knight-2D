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
        }
        if(PlayerPrefs.HasKey(music))
        {
            float musicValue = PlayerPrefs.GetFloat(music);
            SetVolume(music, musicValue);
        }
    }

    public void SetVolume(string name, float value)
    {
        audioMixer.SetFloat(name, value);
        PlayerPrefs.SetFloat(name, value);
    }

    public void SetSoundVolume(float value)
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        SetVolume(sound, value);
        if (value == 0)
        {
            SetActive(soundObj[0]);
            SetInActive(soundObj[1]);
        }
        else
        {
            SetActive(soundObj[1]);
            SetInActive(soundObj[0]);
        }
    }

    public void SetMusicVolume(float value)
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        SetVolume(music, value);
        if (value == 0)
        {
            SetActive(musicObj[0]);
            SetInActive(musicObj[1]);
        }
        else
        {
            SetActive(musicObj[1]);
            SetInActive(musicObj[0]);
        }
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
