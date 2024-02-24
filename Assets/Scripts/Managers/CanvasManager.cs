using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance {  get; private set; }

    [Header("Coins")]
    [SerializeField] private Text coinText;
    
    [Header("Setting")]
    [SerializeField] private GameObject[] settingCV;

    [Header("Press Enter")]
    [SerializeField] private GameObject pressEnter;
    public bool isOpenSettingCV {  get; private set; }

    private int settingID = 0;
    public bool isOpenCV {  get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.keyESC)
        {
            if (isOpenSettingCV)
            {
                CloseSetting(settingID);
            }
            else
            {
                OpenSetting(settingID);
            }
        }
       
    }

    public void LoadScence(string scenceName)
    {
        SceneManager.LoadScene(scenceName);
        Time.timeScale = 1;
        isOpenSettingCV = false;
    }

    public void OpenSetting(int i)
    {
        isOpenSettingCV = true;
        settingCV[i].SetActive(true);
        Time.timeScale = 0;
    }
    public void CloseSetting(int i)
    {
        isOpenSettingCV = false;
        settingCV[i].SetActive(false);
        Time.timeScale = 1;
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
