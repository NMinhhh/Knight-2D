using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager Instance {  get; private set; }
    private GameObject GO;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CreateFloatingText(GameObject textGO, Vector3 textPos, string text, Color newColor, int dir)
    {
        GO = Instantiate(textGO, textPos, Quaternion.identity);
        FloatingText floatingText = GO.GetComponent<FloatingText>();
        floatingText.SetText(text, newColor, dir);
    }
}
