using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class AudioMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    private string sound = "Sound";
    private string music = "Music";
    void Start()
    {
        if (PlayerPrefs.HasKey(sound))
        {
            SetSound(PlayerPrefs.GetFloat(sound));
        }
        if(PlayerPrefs.HasKey(music))
        {
            SetMusic(PlayerPrefs.GetFloat(music));
        }
    }

    public void SetActive(GameObject go)
    {
        go.gameObject.SetActive(true);
    }

    public void SetInActive(GameObject go)
    {
        go.gameObject.SetActive(false);
    }

    public void SetSound(float value)
    {
        audioMixer.SetFloat(sound, value);
        PlayerPrefs.SetFloat(sound, value);
    }

    public void SetMusic(float value)
    {
        audioMixer.SetFloat(music, value);
        PlayerPrefs.SetFloat(music, value);


    }
}
