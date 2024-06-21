using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetGameUI : MonoBehaviour
{
    [Header("Check to reset UI")]
    [SerializeField] private GameObject checkToResetUI;
    [SerializeField] private Button btnYes;
    [SerializeField] private Button btnNo;

    [Header("Reset UI")]
    [SerializeField] private GameObject resetUI;
    [SerializeField] private Text resetText;
    [SerializeField] private float resetTimer;
    private float currentResetTimer;
    private bool isReset;
    [SerializeField] private float delayTimer;
    private float currentDelayTimer;
    [SerializeField] private LoadScene loadScene;

    private void Start()
    {
        OnClickButtonYes();
        OnClickButtonNo();
    }

    private void Update()
    {
        if (isReset)
        {
            ResetProcess();
        }
    }

    void OnClickButtonYes()
    {
        btnYes.onClick.RemoveAllListeners();
        btnYes.onClick.AddListener(() => IsReset());
    }

    void OnClickButtonNo()
    {
        btnNo.onClick.RemoveAllListeners();
        btnNo.onClick.AddListener(() => CloseCheckToResetUI());
    }

    public void OpenCheckToResetGame()
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        checkToResetUI.SetActive(true);
    }

    public void CloseCheckToResetUI()
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        checkToResetUI.SetActive(false);
    }

    void IsReset()
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.Click);
        SoundFXManager.Instance.StopMusic();
        isReset = true;
        Time.timeScale = 1;
        resetUI.SetActive(true);
    }

    public void ResetProcess()
    {
        currentResetTimer += Time.deltaTime;
        if(currentResetTimer > resetTimer)
        {
            SetResetText("Reset game thành công!");
            currentDelayTimer += Time.deltaTime;
            if(currentDelayTimer > delayTimer)
            {
                isReset = false;
                SaveManager.Instance.ResetGame();
                loadScene.Load(SceneIndexs.HOME.ToString());
            }
        }
        else
        {
            SetResetText("Đang reset game, vui lòng đợi...");
        }
    }

    public void SetResetText(string text)
    {
        resetText.text = text;
    }
}
