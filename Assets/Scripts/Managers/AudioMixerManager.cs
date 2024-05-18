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
            SetSound(soundValue);
            CheckVolume(soundObj, soundValue);
        }
        if(PlayerPrefs.HasKey(music))
        {
            float musicValue = PlayerPrefs.GetFloat(music);
            SetMusic(musicValue);
            CheckVolume(musicObj, musicValue);
        }
    }

    void CheckVolume(GameObject[] gameObjects, float value)
    {
        if(value == 0)
        {
            SetActive(gameObjects[0]);
            SetInActive(gameObjects[1]);
        }
        else
        {
            SetActive(gameObjects[1]);
            SetInActive(gameObjects[0]);
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
