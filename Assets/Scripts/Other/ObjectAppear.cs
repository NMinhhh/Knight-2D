using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAppear : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private GameObject light2D;

    [SerializeField] private float timeAppear;
    private SpriteRenderer sprite;
    private Color color;
    float time;
    [SerializeField] private bool isDestroyInAnimation;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        color = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (DayNightCircle.Instance.isNight)
        {
            light2D.SetActive(true);
        }
        else
        {
            light2D.SetActive(false);
        }

        if (isDestroyInAnimation) return;

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

    void Destroy()
    {
        Destroy(gameObject);
    }
}
