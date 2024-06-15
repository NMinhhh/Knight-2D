using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSkillUI : MonoBehaviour
{
    public static MenuSkillUI Instance {  get; private set; }

    [SerializeField] private GameObject menuSkillUI;
    [Header("Energy to change")]
    [SerializeField] private Text energyPriceChange;
    [Header("Info Item")]
    [SerializeField] private Text infoItemText;
    [SerializeField] private ScrollRect scrollRectItem;
    [Header("Next Stage")]
    [SerializeField] private Text nextStageText;

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
        this.menuSkillUI.SetActive(true);
        if (MapManager.Instance.isBoss)
            SetNextStageText(MapManager.Instance.stage + " (Boss)");
        else
            SetNextStageText(MapManager.Instance.stage.ToString());
        this.isMenuOp = true;
        Time.timeScale = 0;
    }

    public void CloseMenuSkill()
    {
        this.menuSkillUI.SetActive(false);
        this.isMenuOp = false;
        Time.timeScale = 1;
    }

    public void SetEnergyPriceChange(int energy)
    {
        this.energyPriceChange.text = energy.ToString();
    }
    public void SetInfoItem(string text)
    {
        this.infoItemText.text = text;
    }

    public void ResetVerticelItem()
    {
        scrollRectItem.verticalNormalizedPosition = 1;
    }

    public void SetNextStageText(string text)
    {
        this.nextStageText.text = "";
        this.nextStageText.text = "Stage : "+ text;
    }
}
