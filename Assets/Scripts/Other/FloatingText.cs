using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float moveDir;
    [SerializeField] private float appearTimeMax;
    private float currentAppearTime;
    [SerializeField] private float disappearTime;
    private TextMeshPro textMesh;
    private Color textColor;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        textColor = textMesh.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(.7f,moveDir) * Time.deltaTime;
        moveDir -= Time.deltaTime;

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

    public void SetText(string text)
    {
        textMesh.SetText(text);
    }

}
