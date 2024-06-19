using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance {  get; private set; }

    [SerializeField] private AudioSource audioSource;
    public enum Sound
    {
        Win,
        Lose,
        Click,
        ClickTab,
        Buy,
        LaserSkill,
        RocketExplosion,
        Lightning,
        DiceHit,
        GunMachine,
        Electric,
        Throw,
        BulletExplodeSkill,
        SawHit,
        LaserGun,
        NormalGun,
        ShotingGun,
        PenetratingGun,
        RoketGun,
        BulletExplodeGun,
    }

    private Dictionary<Sound, float> soundTimerDictionary;

    [SerializeField] private List<SoundAudioClip> soundList;


    private void Awake()
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

    public void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.NormalGun] = 0;
        soundTimerDictionary[Sound.Electric] = 0;
        soundTimerDictionary[Sound.SawHit] = 0;
    }

    public void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            AudioSource audio = Instantiate(audioSource, transform.position, Quaternion.identity);
            audio.clip = GetSound(sound);
            audio.Play();
            float length = audio.clip.length;
            Destroy(audio.gameObject, length);
        }
        
    }

    public bool CanPlaySound(Sound sound)
    {
        switch(sound)
        {
            default:
                return true;
            case Sound.GunMachine:
                return CheckCanPlaySound(sound);
            case Sound.Electric:
                return CheckCanPlaySound(sound);
            case Sound.SawHit:
                return CheckCanPlaySound(sound);
        }
    }

    bool CheckCanPlaySound(Sound sound)
    {
        if (soundTimerDictionary.ContainsKey(sound))
        {
            float lastTimePlayed = soundTimerDictionary[sound];
            float timerMax = GetSound(sound).length;
            if (lastTimePlayed + timerMax < Time.time)
            {
                soundTimerDictionary[sound] = Time.time;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    public AudioClip GetSound(Sound sound)
    {
        foreach(SoundAudioClip s in soundList)
        {
            if(s.sound == sound)
            {
                return s.audioClip;
            }
        }
        return null;
    }
}
[System.Serializable]
public class SoundAudioClip
{
    public SoundFXManager.Sound sound;
    public AudioClip audioClip;
}

