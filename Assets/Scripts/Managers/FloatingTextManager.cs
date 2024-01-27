using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager Instance {  get; private set; }
    private GameObject GO; 


    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void CreateFloatingText(GameObject textGO, Transform textPos, string text, int direction)
    {
        GO = Instantiate(textGO, textPos.position, Quaternion.identity);
        FloatingText floatingText = GO.GetComponent<FloatingText>();
        floatingText.SetText(text, direction);
    }
}
