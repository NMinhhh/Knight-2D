using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBlastSpawnSkill : MonoBehaviour
{
    //WaterBlast Obj
    [SerializeField] private GameObject spawnGo;
    private GameObject go;
    private WaterBlast script;
    //Cooldown
    [SerializeField] private float cooldown;
    private float timer;

    //Info Water
    [SerializeField] private float damage;
    [SerializeField] private Vector2 speed;
    [SerializeField] private float timeLife;

    private int currentDir;

    void Start()
    {
        timer = cooldown;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        timer -= Time.deltaTime;
        if (timer <= 0 && currentDir > 0)
        {
            timer = cooldown;
            SetSkill(currentDir);
        }
    }

    public void AddDirSkill(int i)
    {
        currentDir = i;
    }

    void SetSkill(int level)
    {
        Vector3 pos;
        int amountOfEnemy = 0;
        Collider2D[] enemy = EnemysPosition.Instance.GetEnemysPosition();
        for (int i = 0; i < level; i++)
        {
            if (enemy.Length > 0 && amountOfEnemy < enemy.Length)
            {
                amountOfEnemy++;
                pos = enemy[i].transform.position;
            }
            else
            {
                pos = new Vector3(Random.Range(transform.position.x + 10, transform.position.x - 10), Random.Range(transform.position.y + 10, transform.position.y - 10), 0);
            }
            go = Instantiate(spawnGo, transform.position, Quaternion.identity);
            script = go.GetComponent<WaterBlast>();
            script.SetSkill(damage, Random.Range(speed.x, speed.y), timeLife, pos);
        }
    }
}
