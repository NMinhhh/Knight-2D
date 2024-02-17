using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance {  get; private set; }
    [SerializeField] private GameObject settingCV;
    public bool isOpenSettingCV {  get; private set; }

    public bool isOpenCV {  get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
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
}
