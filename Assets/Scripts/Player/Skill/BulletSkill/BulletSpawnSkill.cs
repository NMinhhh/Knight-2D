using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnSkill : MonoBehaviour
{
    [Header("Component Bullets")]
    [SerializeField] private Transform[] spawnPos;
    [SerializeField] private GameObject bullet;
    private GameObject go;
    private Projectile script;

    [Header("Info Bullet")]
    [SerializeField] private float damge;
    [SerializeField] private Vector2 speed;
    [SerializeField] private float timeLife;

    [Header("Cooldown")]
    [SerializeField] private float cooldown;
    [SerializeField] private int amountBullet;
    private float timer;

    //AmountOf point spawn Bullet
    private int currentPos;


    void Start()
    {
        timer = cooldown;
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && currentPos > 0)
        {
            timer = cooldown;
            for(int i = 0; i < currentPos; i++) 
            {
                SetSkill(spawnPos[i]);
            }
        }
    }


    public void AddAmountPosSkill(int i)
    {
        currentPos = i;
        spawnPos[i - 1].gameObject.SetActive(true);
    }

    void SetSkill(Transform pos)
    {
        float rotationZ;
        Vector3 direction;
        GameObject enemyRam;
        Collider2D[] enemy = EnemysPosition.Instance.GetEnemysPosition();
        for (int i = 0; i < amountBullet; i++)
        {
            if(enemy.Length > 0)
            {
                enemyRam = enemy[Random.Range(0,enemy.Length)].gameObject;
                direction = (enemyRam.transform.position - pos.position).normalized;
                rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            }
            else
            {
                rotationZ = Random.Range(0, 360);
            }
            go = Instantiate(bullet, pos.position, Quaternion.Euler(0, 0, rotationZ));
            script = go.GetComponent<Projectile>();
            script.CreateBullet(damge, Random.Range(speed.x, speed.y), timeLife); 
        }
    }

    

}
