using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondItemUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text value;
    [SerializeField] private Button btn;
    [SerializeField] private Text timerText;
    private int id;
    private TimeSpan timer;
    private bool isProgress;
    double totalSecondLeft;
    DateTime endTimer;

    private void OnEnable()
    {
        StartCoroutine(DisplayUI());
    }

    private void OnDisable()
    {
        StopCoroutine(DisplayUI());
    }

    private void OnDestroy()
    {
        StopCoroutine(DisplayUI());
        Timer.Instance.SetTimeCheck();

    }

    private void Update()
    {
        GameData.Instance.GetDiamondData().SetSecondTimeLeft(id, Mathf.FloorToInt((float)totalSecondLeft));
    }

    public void SetId(int id)
    {
        this.id = id;
    }

    public void SetImage(Sprite sprite)
    {
        this.image.sprite = sprite;
    }

    public void SetValue(int value)
    {
        this.value.text = value.ToString();
    }

    public void OnButtonReward(int i, Action<int> action)
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => action(i));
    }

    public void Reward()
    {
        GameData.Instance.GetDiamondData().GetDiamond(id).secondTimeLeft = 0;
        totalSecondLeft = 0;
        timerText.text = "Nhận";
        btn.interactable = true;
    }

    public void StartTimer(int hour, int minute, int second) 
    {
        if(GameData.Instance.GetDiamondData().GetSecondTimeLeft(id) > 0)
        {
            TimeSpan ts = DateTime.Now - Timer.Instance.timeCheck;
            double passSecond = ts.TotalSeconds;
            double temp = GameData.Instance.GetDiamondData().GetSecondTimeLeft(id) - passSecond;
            if(temp > 0)
            {
                timer = new TimeSpan(0, 0, Mathf.FloorToInt((float)temp));
                endTimer = DateTime.Now.Add(timer);
                totalSecondLeft = timer.TotalSeconds;
                isProgress = true;
                btn.interactable = false;
                return;
            }
            else
            {
                isProgress = false;
                Reward();
                return;
            }
        }
        else
        {
            timer = new TimeSpan(hour, minute, second);
            endTimer = DateTime.Now.Add(timer);
            isProgress = true;
            totalSecondLeft = timer.TotalSeconds;
            btn.interactable = false;
        }
        GameData.Instance.GetDiamondData().SetSecondTimeLeft(id, Mathf.FloorToInt((float)totalSecondLeft));

    }

    public void ResetTimer(int hour, int minute, int second)
    {
        StartTimer(hour, minute, second);
        StartCoroutine(DisplayUI());
    }

    public IEnumerator DisplayUI()
    {
        string text;
        DateTime start = DateTime.Now;
        TimeSpan timeLeft = endTimer - start;
        totalSecondLeft = timeLeft.TotalSeconds;
        while (isProgress)
        {
            text = "";
            if (totalSecondLeft > 1)
            {
                if(timeLeft.Hours != 0)
                {
                    text += $"{timeLeft.Hours:00}h{timeLeft.Minutes:00}m";
                    yield return new WaitForSeconds(timeLeft.Seconds);
                }else if(timeLeft.Minutes != 0)
                {
                    TimeSpan ts = TimeSpan.FromSeconds(totalSecondLeft);
                    text += $"{ts.Minutes:00}m{ts.Seconds:00}s";
                }
                else
                {
                    text += $"{Mathf.FloorToInt((float)totalSecondLeft)}s";
                }
                timerText.text = text;
                totalSecondLeft -= Time.deltaTime;
                yield return null;
            }
            else
            {
                isProgress = false;
                Reward();
            }
        }
        yield return null;
    }
}
