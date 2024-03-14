using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpawnSkill : MonoBehaviour
{
    //LightningObj
    [SerializeField] private GameObject lightning;
    private GameObject go;
    private Lightning script;

    //Cooldown and damage
    [SerializeField] private float damage;
    [SerializeField] private float cooldown;
    private float timer;

    private int amountLightning;
    

    void Start()
    {
        timer = cooldown;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && amountLightning > 0)
        {
            timer = cooldown;
            SetSkill(amountLightning);
        }
    }

    public void SetAmount(int amount)
    {
        amountLightning = amount;
    }

    void SetSkill(int amount)
    {
        Vector3 pos;
        Collider2D[] enemy = EnemysPosition.Instance.GetEnemysPosition();
        int amountOfEnemy = 0;
        for (int i = 0; i < amount; i++)
        {
            if (enemy.Length > 0 && amountOfEnemy < enemy.Length)
            {
                pos = enemy[i].transform.position;
                amountOfEnemy++;
            }
            else
            {
                pos = new Vector3(Random.Range(transform.position.x + 10, transform.position.x - 10), Random.Range(transform.position.y + 8, transform.position.y - 8), 0);
            }
            go = Instantiate(lightning, pos, Quaternion.identity);
            script = go.GetComponent<Lightning>();
            script.Set(damage);
        }
    }
}
