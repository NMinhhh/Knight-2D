using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public void Load(string name)
    {
        //SaveManager.Instance.SaveGame();
        Time.timeScale = 1.0f;
        Loader.LoadScene(name);
    }

    public void LoadAndPlaySound(string name)
    {
        //SaveManager.Instance.SaveGame();
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        Time.timeScale = 1.0f;
        Loader.LoadScene(name);
    }
}
