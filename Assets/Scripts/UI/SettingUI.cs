using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private GameObject settingUI;
    [SerializeField] private bool isTimeStop;
    void Start()
    {
        Time.timeScale = 1;
        settingUI.SetActive(false);
    }

    public void OpenSetting()
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        CanvasManager.Instance.OpenUI(settingUI);
        TimeStop();
    }

    void TimeStop()
    {
        if (isTimeStop)
        {
            Time.timeScale = 0;
            SoundFXManager.Instance.StopAudio();
        }
        else
            Time.timeScale = 1;
    }

    public void CloseSetting()
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        if (GameObject.Find("MenuSkill") != null)
        {
            if (MenuSkillUI.Instance.isMenuOp)
                Time.timeScale = 0;
            else
            {
                Time.timeScale = 1;
            }
        }
        else
        {
            Time.timeScale = 1;
        }
        if (isTimeStop)
        {
            SoundFXManager.Instance.PlayAudio();
        }
        CanvasManager.Instance.CloseUI(settingUI);
    }
}
