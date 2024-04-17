using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private GameObject settingUI;

    void Start()
    {
        Time.timeScale = 1;
    }

    public void OpenSetting()
    {
        CanvasManager.Instance.OpenUI(settingUI);
        Time.timeScale = 0;
    }

    public void CloseSetting()
    {
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
        CanvasManager.Instance.CloseUI(settingUI);
    }
}
