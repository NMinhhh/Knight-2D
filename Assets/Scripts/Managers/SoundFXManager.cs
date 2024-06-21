using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance {  get; private set; }

    [SerializeField] private AudioSource audioSource;
    public enum Sound
    {
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

    private List<AudioSource> allAudioSources = new List<AudioSource>();

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

    public string GetTag(SoundAudioClip.AudioType type)
    {
        switch(type)
        {
            default:
                return null;
            case SoundAudioClip.AudioType.UnTag:
                return null;
            case SoundAudioClip.AudioType.UI:
                return "SoundUI";
            case SoundAudioClip.AudioType.GamePlay:
                return "SoundGamePlay"; 
        }
    }

    public void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            AudioSource audio = Instantiate(audioSource, transform.position, Quaternion.identity);
            audio.gameObject.tag = GetTag(GetAudioType(sound));
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

    public SoundAudioClip.AudioType GetAudioType(Sound sound)
    {
        foreach (SoundAudioClip s in soundList)
        {
            if (s.sound == sound)
            {
                return s.audioType;
            }
        }
        return SoundAudioClip.AudioType.UnTag;
    }

    public void StopMusic()
    {
        allAudioSources.Clear();
        GameObject[] music = GameObject.FindGameObjectsWithTag("Music");
        foreach (GameObject m in music)
        {
            allAudioSources.Add(m.GetComponent<AudioSource>());
        }

        foreach (var a in allAudioSources)
        {
            a.Pause();
        }

        allAudioSources.Clear();
    }

    public void PlayMusic()
    {
        allAudioSources.Clear();
        GameObject[] music = GameObject.FindGameObjectsWithTag("Music");
        foreach (GameObject m in music)
        {
            allAudioSources.Add(m.GetComponent<AudioSource>());
        }

        foreach (var a in allAudioSources)
        {
            a.Play();
        }

        allAudioSources.Clear();
    }

    public void StopAudio()
    {
        allAudioSources.Clear();
        GameObject[] music = GameObject.FindGameObjectsWithTag("Music");
        GameObject[] soundGamePlay = GameObject.FindGameObjectsWithTag("SoundGamePlay");
        foreach (GameObject m in music)
        {
            allAudioSources.Add(m.GetComponent<AudioSource>());
        }

        foreach (GameObject s in soundGamePlay)
        {
            allAudioSources.Add(s.GetComponent<AudioSource>());
        }

        foreach(var a in allAudioSources)
        {
            a.Pause();
        }

        allAudioSources.Clear();
    }

    public void PlayAudio()
    {
        allAudioSources.Clear();
        GameObject[] music = GameObject.FindGameObjectsWithTag("Music");
        GameObject[] soundGamePlay = GameObject.FindGameObjectsWithTag("SoundGamePlay");
        foreach (GameObject m in music)
        {
            allAudioSources.Add(m.GetComponent<AudioSource>());
        }

        foreach (GameObject s in soundGamePlay)
        {
            allAudioSources.Add(s.GetComponent<AudioSource>());
        }

        foreach (var a in allAudioSources)
        {
            a.Play();
        }

        allAudioSources.Clear();
    }
}
[System.Serializable]
public class SoundAudioClip
{
    public enum AudioType
    {
        UnTag,
        UI,
        GamePlay,

    }
    public SoundFXManager.Sound sound;
    public AudioClip audioClip;
    public AudioType audioType;
}

