using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager Instance {  get; private set; }
    FloatingText floatingText;
    private GameObject GO; 


    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void CreateFloatingText(GameObject textGO, Transform textPos, string text)
    {
        GO = Instantiate(textGO, textPos.position, Quaternion.identity);
        floatingText = GO.GetComponent<FloatingText>();
        floatingText.SetText(text);
    }
}
