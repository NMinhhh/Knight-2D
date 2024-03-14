using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderCallBack : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private float timerLoader;
    [SerializeField] private Color[] color;
    [Range(0f, 1f)]
    [SerializeField] private float time;
    private float timerCur;
    private bool isEffect;

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
            if (!isEffect) { 
                isEffect = true;
                StartCoroutine(ChangeColor());
            }
            if (fill.fillAmount >= 1)
            {
                Loader.AllowSceneActivation();
            }
        }   
    }

    IEnumerator ChangeColor()
    {
        Color newColor = color[Random.Range(0, color.Length)];  
        fill.color = newColor;
        yield return new WaitForSeconds(time);
        StartCoroutine(ChangeColor());
    }
}
