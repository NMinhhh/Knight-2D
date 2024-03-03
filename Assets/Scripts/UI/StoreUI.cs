using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreUI : MonoBehaviour
{
    [SerializeField] private GameObject goUI;
    
    public void OpenUI()
    {
        CanvasManager.Instance.OpenUI(goUI);
    }

    public void CloseUI()
    {
        CanvasManager.Instance.CloseUI(goUI);
    }
}
