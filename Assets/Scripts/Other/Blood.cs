using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    [SerializeField] private float timeAppear;
    private SpriteRenderer sprite;
    private Color color;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        color = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(time < timeAppear)
        {
            time += Time.deltaTime;
        }
        else
        {
            color.a -= Time.deltaTime;
            sprite.color = color;
            if(color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
