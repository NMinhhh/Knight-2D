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

    private int level;
    

    void Start()
    {
        timer = cooldown;

    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && level > 0)
        {
            timer = cooldown;
            SetSkill(level);
        }
    }

    public void LevelUp(int level)
    {
        this.level = level;
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
                pos = new Vector3(Random.Range(transform.position.x + 5, transform.position.x - 5), Random.Range(transform.position.y + 5, transform.position.y - 5), 0);
            }
            go = Instantiate(lightning, new Vector3(pos.x, pos.y + 20, 0), Quaternion.identity);
            script = go.GetComponent<Lightning>();
            script.Set(pos,damage);
        }
    }
}
