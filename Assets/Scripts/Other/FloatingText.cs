using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float appearTimeMax;
    [SerializeField] private float disappearTime;
    public static int sortingOrder;
    private Vector2 moveDir;
    private float currentAppearTime;
    private TextMeshPro textMesh;
    private Color textColor;
    // Start is called before the first frame update
    void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }
    public void SetText(string text, Color newColor, int dir)
    {
        textMesh.SetText(text);
        textMesh.color = newColor;
        textColor = textMesh.color;
        moveDir = new Vector2(.3f * dir, .3f) * 20f;
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)moveDir * Time.deltaTime;
        moveDir -= moveDir * 15f * Time.deltaTime;

        if(currentAppearTime < appearTimeMax *.5f)
        {
            transform.localScale += Vector3.one * Time.deltaTime;
        }
        else
        {
            transform.localScale -= Vector3.one * Time.deltaTime;
        }
        currentAppearTime += Time.deltaTime;
        if(currentAppearTime >= appearTimeMax)
        {
            textColor.a -= disappearTime * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }

    }

   
}
