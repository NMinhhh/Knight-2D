using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    [SerializeField] private Text text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = MapManager.Instance.energy.ToString();
    }
}
