using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoShootUI : MonoBehaviour
{
    [SerializeField] private HandleRotation handleRotation;
    [SerializeField] private Slider slider;
    [SerializeField] private float time;
    [SerializeField] private Outline outline;
    private bool isTurnAuto;
    private bool isSetValue;
    void Update()
    {

        if(isSetValue && isTurnAuto)
        {
            slider.value = 1;
            outline.effectColor = Color.green;
            if(slider.value == 1)
                isSetValue = false;
        }else if(isSetValue && !isTurnAuto)
        {
            slider.value = 0;
            outline.effectColor = Color.gray;
            if( slider.value == 0)
                isSetValue = false;
        }
    }
    

    public void ClickAuto()
    {
        handleRotation.TurnAuto();
        isTurnAuto = handleRotation.auto;
        isSetValue = true;
    }
}
