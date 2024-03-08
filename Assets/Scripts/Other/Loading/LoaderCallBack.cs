using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderCallBack : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private float timerLoader;

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
            fill.fillAmount += Time.deltaTime;
            if (fill.fillAmount >= 1)
            {
                Loader.AllowSceneActivation();
            }
        }

       
        
    }
}
