using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private Text timeText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = $"{MapManager.Instance.minutes:00}:{MapManager.Instance.seconds:00}";
    }
}
