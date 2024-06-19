using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }
    public DateTime timeCheck {  get; private set; }

    void Awake()
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

    public void SetTimeCheck()
    {
        timeCheck = DateTime.Now;
    }

    private void OnApplicationQuit()
    {
        GameData.Instance.GetDiamondData().ResetSecond();
    }
}
