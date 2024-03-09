using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public void Load(string name)
    {
        Time.timeScale = 1.0f;
        Loader.LoadScene(name);
    }
}
