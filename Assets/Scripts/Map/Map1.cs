using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1 : MonoBehaviour
{
    [SerializeField] private int level;
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
    [SerializeField] private CinemachineConfiner confiner;
    [SerializeField] private PolygonCollider2D polygonCollider;
    [SerializeField] private PolygonCollider2D polygonColliderCur;
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
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private GameObject[] enemySpawn;
    private bool isBossAppear;
    private bool isAttackBoss;
    private bool isWin;
    private bool isBoss;

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
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        minute = GameManager.Instance.minutes;
        levelUp = GameManager.Instance.level;
        timeCur -= Time.deltaTime;
        if (isWin)
        {
            roomBoss.SetActive(false);
            confiner.m_BoundingShape2D = polygonColliderCur;
            Debug.Log("You win!!!");
            isWin = false;
            isBossAppear = false;
            isAttackBoss = false;
            if (!CoinManager.Instance.GetMapState())
            {
                Debug.Log("New Map unlock");
                CoinManager.Instance.AddMapWin(level);
                CoinManager.Instance.AddMapUnlock(level + 1);
                CoinManager.Instance.SetMapState(true);
            }
        }
        else
        {
            CheckBossAppear();
            //Spawn enemy
            if (timeCur < 0 && !isBossAppear)
            {
                ControlSpawnEnemy();
                timeCur = cooldown;
            }
            else if (isBossAppear && !isAttackBoss)
            {
                //All enemies die
                EnemyAllDeath();

                //Warning UI
                warningTimeCur += Time.deltaTime;
                warning.SetActive(true);
                //Boss appear
                if (warningTimeCur >= warningTime)
                {
                    isAttackBoss = true;
                    warning.SetActive(false);
                    go = Instantiate(boss, bossPoint.position, Quaternion.identity);
                }
            }else if (isAttackBoss)
            {
                if (go == null)
                {
                    isWin = true;
                }
            }

        }
    }


    void CheckBossAppear()
    {
        if (GameManager.Instance.kill == 3000 && !isBoss)
        {
            roomBoss.SetActive(true);
            confiner.m_BoundingShape2D = polygonCollider;
            isBossAppear = true;
            isBoss = true;
        }
    }

    void ControlSpawnEnemy()
    {
        if(levelCur < levelUp)
        {
            levelCur = levelUp;
            if(cooldown > 0.2f)
                cooldown = Mathf.Clamp(cooldown -  0.1f, 0.2f, maxCooldown);
        }
        int i = Random.Range(0, enemyNor.Length);
        Spawn(enemyNor[i], 1);
        amountOfEnemyNor++;
        if(levelUp > 5 && amountOfEnemyNor > 3)
        {
            Spawn(enemyMed[0], 1);
            amountOfEnemyNor = 0;
        }

    }

    void EnemyAllDeath()
    {
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        attackDetail.damage = 100;
        attackDetail.attackDir = transform;
        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].transform.SendMessage("Damage", attackDetail);
            if (enemies[i] == null)
            {
                enemies.Remove(enemies[i]);
            }
        }
        isBossAppear = true;
        enemies.Clear();

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
