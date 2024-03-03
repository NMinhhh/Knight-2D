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
        Time.timeScale = 0;
        isSeting = true;
    }

    public void CloseSetting()
    {
        CanvasManager.Instance.CloseUI(settingUI);
        if (GameObject.Find("MenuSkill") != null)
        {
            if (MenuSkillUI.Instance.isMenuOp)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 1;
        }
        isSeting = false;
    }
}
