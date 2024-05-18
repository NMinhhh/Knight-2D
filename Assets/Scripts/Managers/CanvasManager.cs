using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public RectTransform diamondRect;
    public RectTransform energyRect;
    public GameObject energyUI;
    public static CanvasManager Instance {  get; private set; }

    public bool isOpenUI {  get; private set; }

    void Awake()
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

    public void EnergyUIActive()
    {
        energyUI.SetActive(true);
    }

    public void EnergyUIInActive()
    {
        energyUI.SetActive(false);
    }

    public Vector3 GetDestinationEnergyPoint()
    {
        if (!energyRect) return Vector3.zero;

        Vector3[] v = new Vector3[4];
        energyRect.GetWorldCorners(v);
        return v[0];//bottom  left
    }

    public Vector3 GetDestinationDiamondPoint()
    {
        if(!diamondRect) return Vector3.zero;

        Vector3[] v = new Vector3[4];
        diamondRect.GetWorldCorners(v);
        return v[0];//bottom  left
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        EnergyUIInActive();
    }

    public void OpenUI(GameObject go)
    {
        go.SetActive(true);
        isOpenUI = true;
    }

    public void CloseUI(GameObject go)
    {
        go.SetActive(false);
        isOpenUI = false;
    }
}
