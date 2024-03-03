using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSkillUI : MonoBehaviour
{
    public static MenuSkillUI Instance {  get; private set; }

    [SerializeField] private GameObject menuSkillUI;

    public bool isMenuOp {  get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenMenuSkill()
    {
        menuSkillUI.SetActive(true);
        isMenuOp = true;
        Time.timeScale = 0;
    }

    public void CloseMenuSkill()
    {
        menuSkillUI.SetActive(false);
        isMenuOp = false;
        Time.timeScale = 1;
    }
}
