using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance {  get; private set; }

    [Header("Coins")]
    [SerializeField] private Text coinText;
    
    [Header("Setting")]
    [SerializeField] private GameObject settingCV;

    [Header("Press Enter")]
    [SerializeField] private GameObject pressEnter;
    public bool isOpenSettingCV {  get; private set; }

    public bool isOpenCV {  get; private set; }
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.keyESC)
        {
            isOpenSettingCV = !isOpenSettingCV; 
            settingCV.SetActive(isOpenSettingCV);
        }
        if(isOpenSettingCV)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void OpenUI(GameObject go)
    {
        go.SetActive(true);
        isOpenCV = true;
    }

    public void CloseUI(GameObject go)
    {
        go.SetActive(false);
        isOpenCV = false;
    }

    public void SetCoinsUI(string coinString)
    {
        coinText.text = coinString;
    }

    public void EnterActive()
    {
        pressEnter.SetActive(true);
    }

    public void ExitActive()
    {
        pressEnter.SetActive(false);
    }
}
