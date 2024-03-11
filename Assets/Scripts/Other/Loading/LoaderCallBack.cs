using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderCallBack : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private float timerLoader;
    private float timerCur;

    private bool isFirstUpdate = true;
    private void Start()
    {
        fill.fillAmount = 0;
    }

    private void Update()
    {
        
        if(isFirstUpdate)
        {
            isFirstUpdate = false;
            Loader.LoaderCallBack();
        }
        if (Loader.isLoadFill)
        {
            timerCur += Time.deltaTime;
            fill.fillAmount = timerCur / timerLoader;
            if (fill.fillAmount >= 1)
            {
                Loader.AllowSceneActivation();
            }
        }

       
        
    }
}
