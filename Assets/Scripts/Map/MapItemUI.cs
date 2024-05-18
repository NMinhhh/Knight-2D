using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapItemUI : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject unlockObj;
    [SerializeField] private LoadScene loadScene;
    
    public void SetLevelText(string levelText)
    {
        this.levelText.text = levelText;
    }

    public void OnMapUnlock()
    {
        unlockObj.SetActive(false);
    }

    public void OnMapLock()
    {
        unlockObj.SetActive(true);
    }

    public void OnMapSelected(int level, Action<int> action)
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => action(level));
        btn.onClick.AddListener(() => loadScene.Load(SceneIndexs.GAMEPLAY.ToString()));
    }
}
