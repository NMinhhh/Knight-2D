using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] private GameObject stageMes;
    [SerializeField] private Text stageMesText;
    [SerializeField] private Text stageText;

    private int currentStage;

    [SerializeField] private GameObject warningBossUI;

    public void OpenStageUI()
    {
        GameManager.Instance.StageLevelUp();
        currentStage = GameManager.Instance.stage;
        stageMesText.text = "Stage " + currentStage;
        stageText.text = currentStage.ToString();
        stageMes.SetActive(true);
    }

    public void CloseStageUI()
    {
        stageMes.SetActive(false);
    }

    public void OpenWarningBossUI()
    {
        warningBossUI.SetActive(true);
    }
    
    public void CloseWarningBossUI()
    {
        warningBossUI.SetActive(false);
    }
}
