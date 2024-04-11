using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1 : MonoBehaviour
{
    //Level player
    [SerializeField] private int mapLevel;

    //Number of enemy dead to spawn Boss
    [SerializeField] private int amountOfKill;

    //Number of enemy dead to spawn enemy big
    [SerializeField] private int amountOfKillEnemy;
    private int amountOfKillEnemyCur;
  

    [Header("Position spawn enemy")]
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private List<Vector2> sizeSpawnPoint;
    Vector2[] pos;
    [Space]
    [Space]

    [Header("Warning UI")]
    [SerializeField] private GameObject roomBoss;
    [SerializeField] private GameObject warning;
    [SerializeField] private Transform bossPoint;
    [SerializeField] private CinemachineConfiner confiner;
    [SerializeField] private PolygonCollider2D polygonCollider;
    [SerializeField] private PolygonCollider2D polygonColliderCur;
    [SerializeField] private float warningTime;
    private float warningTimeCur;
    [Space]

    [Header("Enemy")]
    [SerializeField] private GameObject[] enemyNor;
    [SerializeField] private GameObject[] enemyMed;
    private int amountOfEnemyNor;

    //Enemy Big
    [SerializeField] private GameObject enemyBig;
    private bool isEnemyBig;
    private GameObject enemyBigGo;

    //Boss
    [SerializeField] private GameObject boss;
    private GameObject go;
    private List<GameObject> enemies;
    private bool isBossAppear;
    private bool isAttackBoss;
    private bool isBoss;

    //cooldown spawn enemy
    [SerializeField] private float cooldown;
    private float maxCooldown;
    private float timeCur;

    private bool isWin;


    //Time in game
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
        amountOfKillEnemyCur = amountOfKillEnemy;
    }

    // Update is called once per frame
    void Update()
    {
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
                CoinManager.Instance.AddMapWin(mapLevel);
                CoinManager.Instance.AddMapUnlock(mapLevel + 1);
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
        if (GameManager.Instance.kill == amountOfKill && !isBoss)
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
            amountOfKillEnemy++;
        }
        amountOfKillEnemyCur--;
        if (amountOfKillEnemyCur <= 0 && levelCur >= 10)
        {
            enemyBigGo = Instantiate(enemyBig, GetPos(), Quaternion.identity);
            isEnemyBig = true;
            amountOfKillEnemyCur = amountOfKillEnemy;
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
        enemies.Clear();

    }



    void Spawn(GameObject gameObject, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnerManager.Instance.SpawnEnemy(gameObject, GetPos());
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
