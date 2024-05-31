using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance {  get; private set; }

    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void CreateAudioClip(AudioClip clip, Transform pos, float volume)
    {
        AudioSource audio = Instantiate(audioSource, pos.position, Quaternion.identity);
        audio.clip = clip;
        audio.volume = volume;
        audio.Play();
        float length = audio.clip.length;
        Destroy(audio.gameObject, length);
    }
}
