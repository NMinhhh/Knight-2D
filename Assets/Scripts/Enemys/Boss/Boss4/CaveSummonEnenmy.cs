using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSummonEnenmy : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private GameObject light2D;

    [Header("Cave Info")]
    [SerializeField] private GameObject[] go;
    [SerializeField] private int amount;
    [SerializeField] private float cooldown;
    private float timer;

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

        if (amount <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            timer += Time.deltaTime;
            if(timer > cooldown)
            {
                timer = 0;
                Spawn();
            }
        }
    }

    void Spawn()
    {
        Instantiate(go[Random.Range(0, go.Length)], transform.position, Quaternion.identity);
        amount--;
    }

    public void CreateCave(GameObject[] go, int amount, float cooldown)
    {
        this.go = go;
        this.amount = amount;
        this.cooldown = cooldown;
    }
}
