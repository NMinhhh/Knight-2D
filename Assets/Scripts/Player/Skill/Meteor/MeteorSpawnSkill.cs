using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawnSkill : MonoBehaviour
{
    //Info
    [SerializeField] private GameObject meteorGo;
    private Meteor script;
    private GameObject go;
    //Cool down
    [SerializeField] private float cooldown;
    private float timer;

    //Dir spawn Obj
    [SerializeField] private int currentDir;

    //Info Rocket
    [SerializeField] private float damage;
    [SerializeField] private Vector2 speed;
    [SerializeField] private float timeLife;
    void Start()
    {
        timer = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0 && currentDir > 0)
        {
            SetSkill(currentDir);
            timer = cooldown;
        }
    }

    public void AddDir(int level)
    {
        currentDir = level;
    }

    void SetSkill(int dir)
    {
        float rotationZ;
        Vector3 direction;
        int amountOfEnemy = 0;
        GameObject enemyRam;
        Collider2D[] enemys = EnemysPosition.Instance.GetEnemysPosition();
        for (int i = 0; i < dir; i++)
        {
            if (enemys.Length > 0 && amountOfEnemy < enemys.Length)
            {
                amountOfEnemy++;
                enemyRam = enemys[i].gameObject;
                direction = (enemyRam.transform.position - transform.position).normalized;
                rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            }
            else
            {
                rotationZ = Random.Range(0, 360);
            }
            go = Instantiate(meteorGo, transform.position, Quaternion.Euler(0, 0, rotationZ));
            script = go.GetComponent<Meteor>();
            script.CreateMeteor(damage, Random.Range(speed.x, speed.y), timeLife);
        }
    }
}
