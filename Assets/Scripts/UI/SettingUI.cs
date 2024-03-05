using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private GameObject settingUI;

    private bool isSeting;
    void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (InputManager.Instance.keyESC)
        {
            if(!isSeting)
                OpenSetting();
            else
                CloseSetting();
        }
    }

    public void OpenSetting()
    {
        CanvasManager.Instance.OpenUI(settingUI);
        isSeting = true;
        Time.timeScale = 0;
    }

    public void CloseSetting()
    {
        isSeting = false;
        if (GameObject.Find("MenuSkill") != null)
        {
            if (MenuSkillUI.Instance.isMenuOp)
                Time.timeScale = 0;
            else
            {
                Time.timeScale = 1;
                InputManager.Instance.MouseShoting();
            }
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
