using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1 : MonoBehaviour
{

    [Header("Position spawn enemy")]
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private List<Vector2> sizeSpawnPoint;
    Vector2[] pos;
    [Space]
    [Space]
    [Header("Warning UI")]
    [SerializeField] private GameObject roomBoss;
    [SerializeField] private GameObject warning;
    [SerializeField] private float warningTime;
    [SerializeField] private Transform bossPoint;
    private float warningTimeCur;
    [Space]

    [Header("Enemy")]
    [SerializeField] private GameObject[] enemyNor;
    [SerializeField] private GameObject[] enemyMed;
    [SerializeField] private GameObject boss;
    private int amountOfEnemyNor;
    private GameObject go;
    [Space]

    //cooldown spawn enemy
    [SerializeField] private float cooldown;
    private float maxCooldown;
    private float timeCur;

    //Boss
    [SerializeField] private GameObject[] enemySpawn;
    private bool isAttackBoss;
    private bool isBossAppear;

    //Time in game
    private int minute;
    private int levelUp;
    private int levelCur;
    AttackDetail attackDetail;

    void Start()
    {
        pos = new Vector2[4];
        maxCooldown = cooldown;
        timeCur = 0;
        levelCur = GameManager.Instance.level;
        levelUp = GameManager.Instance.level;
    }

    // Update is called once per frame
    void Update()
    {
        minute = GameManager.Instance.minutes;
        levelUp = GameManager.Instance.level;
        timeCur -= Time.deltaTime;

        if (minute == 1 && !isAttackBoss)
        {
            roomBoss.SetActive(true);
            isBossAppear = true;
            isAttackBoss = true;
        }

        if (isBossAppear)
        {
            //EnemyAllDeath();
            warningTimeCur += Time.deltaTime;
            warning.SetActive(true);
            if (warningTimeCur >= warningTime)
            {
                isBossAppear = false;
                warning.SetActive(false);
                go = Instantiate(boss, bossPoint.position, Quaternion.identity);
            }
        }

        if (timeCur < 0 && !isAttackBoss)
        {
            ControlSpawnEnemy();
            timeCur = cooldown;
        }

    }



    void ControlSpawnEnemy()
    {
       
        if(levelCur < levelUp && cooldown > 0.2f)
        {
            cooldown = Mathf.Clamp(cooldown -  0.1f, 0.2f, maxCooldown);
            levelCur = levelUp;
        }
        int i = Random.Range(0, enemyNor.Length);
        go = Instantiate(enemyNor[i], GetPos(), Quaternion.identity);
        //Spawn(enemyNor[i], 1);
        amountOfEnemyNor++;
        if(levelUp > 5 && amountOfEnemyNor > 3)
        {
            //Spawn(enemyMed[0], 1);
            go = Instantiate(enemyNor[i], GetPos(), Quaternion.identity);
            amountOfEnemyNor = 0;
        }

    }

    void EnemyAllDeath()
    {
        enemySpawn = GameObject.FindGameObjectsWithTag("Enemy");
        attackDetail.damage = 100;
        attackDetail.attackDir = transform;
        for(int i = 0; i < enemyNor.Length; i++)
        {
            enemySpawn[i].transform.SendMessage("Damage", attackDetail);
        }
    }

    

    void Spawn(GameObject gameObject, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnerManager.Instance.SpawnItem(gameObject, GetPos());
        }
    }

    Vector2 GetPos()
    {

        pos[0] = new Vector2(Random.Range(spawnPoint[0].position.x + sizeSpawnPoint[0].x, spawnPoint[0].position.x + sizeSpawnPoint[0].y), spawnPoint[0].position.y);
        pos[1] = new Vector2(spawnPoint[1].position.x, Random.Range(spawnPoint[1].position.y + sizeSpawnPoint[1].x, spawnPoint[1].position.y + sizeSpawnPoint[1].y));
        pos[2] = new Vector2(Random.Range(spawnPoint[2].position.x + sizeSpawnPoint[0].x, spawnPoint[2].position.x + sizeSpawnPoint[0].y), spawnPoint[2].position.y);
        pos[3] = new Vector2(spawnPoint[3].position.x, Random.Range(spawnPoint[3].position.y + sizeSpawnPoint[1].x, spawnPoint[3].position.y + sizeSpawnPoint[1].y));
        return pos[Random.Range(0, pos.Length)];
    }
}
