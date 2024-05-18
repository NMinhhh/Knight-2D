using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map1 : MonoBehaviour
{
    [Header("Map Level")]
    [SerializeField] private int mapLevel;
    [Space]
    [Space]

    [Header("Position spawn enemy")]
    [SerializeField] private Transform[] spawnPoint;
    Vector2 pos;
    [Space]
    [Space]

    [Header("Stage")]
    [SerializeField] private GameObject stageUI;
    [SerializeField] private Text stageTextUI;
    [SerializeField] private Text stageText;
    [SerializeField] private int maxStage;
    private int currentStage;
    [SerializeField] private float cooldownstartStageTimer;
    private float startStageTimer;
    [SerializeField] private float cooldownEndStageTimer;
    private float endStageTimer;
    private bool isChangeStage;
    [Space]
    [Space]

    [Header("SpawnEnemy")]
    [SerializeField] private GameObject iconSpawner;
    SpriteRenderer spriteRenderer;
    private GameObject GO;
    [Space]

    private static int sortingOrder = 0;

    [Header("Enemy1")]
    [SerializeField] private GameObject[] enemy1;
    [SerializeField] private int amountOfEnemy1;
    private int currentAmoutOfEnemy1;
    [SerializeField] private float cooldownEnemy1;
    private float startTime1;
    [Space]
    [Space]

    [Header("Enemy2")]
    [SerializeField] private GameObject[] enemy2;
    [SerializeField] private int amountOfEnemy2;
    [SerializeField] private int stageAppearEnemy2;
    private int currentAmoutOfEnemy2;
    [SerializeField] private float cooldownEnemy2;
    private float startTime2;
    [Space]
    [Space]

    [Header("Enemy3")]
    [SerializeField] private GameObject[] enemy3;
    [SerializeField] private int amountOfEnemy3;
    [SerializeField] private int stageAppearEnemy3;
    private int currentAmoutOfEnemy3;
    [SerializeField] private float cooldownEnemy3;
    private float startTime3;
    [Space]
    [Space]

    [Header("Boss")]
    [SerializeField] private GameObject[] boss;
    [SerializeField] private Transform pointBossSpawn;
    private GameObject go;
    [SerializeField] private int[] stageBossApears;
    [SerializeField] private GameObject warning;
    [SerializeField] private float warningTime;
    private float timer;
    private bool bossStage;
    private bool isBoss;
    private int bossId;

    private List<GameObject> enemies;

    private List<GameObject> items;

    [SerializeField] private int amountAllEnemySpawn;

    AttackDetail attackDetail;

    void Start()
    {
        startTime1 = Time.deltaTime;
        enemies = new List<GameObject>();
        items = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!bossStage)
        {
            if(amountAllEnemySpawn > 0)
            {
                ControlSpawn();
            }else
            {
                if (CheckEnemyToChangeStage() && !isChangeStage)
                {
                    RecieveItem();
                    endStageTimer -= Time.deltaTime;
                    if(endStageTimer <= 0)
                    {
                        startStageTimer = Time.time;
                        isChangeStage = true;
                        SendMessageStage();
                        endStageTimer = cooldownEndStageTimer;
                    }

                }
            }
            if(Time.time >= startStageTimer + cooldownstartStageTimer && isChangeStage)
            {
                ChangeState();
            }
        }
        else
        {
            timer += Time.deltaTime;
            if(timer >= warningTime && !isBoss)
            {
                warning.SetActive(false);
                go = Instantiate(boss[bossId], pointBossSpawn.position, Quaternion.identity);
                isBoss = true;
            }
            if (isBoss)
            {
                if(go == null){
                    isBoss = false;
                    timer = 0;
                    bossStage = false;
                }
            }
        }
       
    }


    void ControlSpawn()
    {
        if (Time.time >= startTime1 + cooldownEnemy1 && currentAmoutOfEnemy1 > 0)
        {
            currentAmoutOfEnemy1--;
            amountAllEnemySpawn--;
            startTime1 = Time.time;
            StartCoroutine(SpawnEnemy(enemy1[Random.Range(0, enemy1.Length)], GetPos()));
        }
        if (currentStage < stageAppearEnemy2)
            return;
        if (Time.time >= startTime2 + cooldownEnemy2 && currentAmoutOfEnemy2 > 0)
        {
            currentAmoutOfEnemy2--;
            amountAllEnemySpawn--;
            startTime2 = Time.time;
            StartCoroutine(SpawnEnemy(enemy2[Random.Range(0, enemy2.Length)], GetPos()));
        }
        if(currentStage < stageAppearEnemy3)
            return;
        if (Time.time >= startTime3 + cooldownEnemy3 && currentAmoutOfEnemy3 > 0)
        {
            currentAmoutOfEnemy3--;
            amountAllEnemySpawn--;
            startTime3 = Time.time;
            StartCoroutine(SpawnEnemy(enemy3[Random.Range(0, enemy3.Length)], GetPos()));
        }
        
    }

    bool CheckEnemyToChangeStage()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }
        return enemies.Count == 0;
    }

    void SendMessageStage()
    {
        GameManager.Instance.StageLevelUp();
        currentStage = GameManager.Instance.stage;
        stageTextUI.text = "Stage " + currentStage;
        stageText.text = currentStage.ToString();
        stageUI.SetActive(true);
    }

    void ChangeState()
    {
        if (stageBossApears[0] == currentStage)
        {
            bossStage = true;
            warning.SetActive(true);
            bossId = 0;
        }else if (stageBossApears[1] == currentStage)
        {
            bossStage = true;
            warning.SetActive(true);
            bossId = 1;
        }
        else
        {
            currentAmoutOfEnemy1 = amountOfEnemy1 * currentStage;

            if (currentStage >= stageAppearEnemy2)
                currentAmoutOfEnemy2 = amountOfEnemy2 * (currentStage - stageAppearEnemy2 + 1);

            if (currentStage >= stageAppearEnemy3)
                currentAmoutOfEnemy3 = amountOfEnemy3 * (currentStage - stageAppearEnemy3 + 1);

            amountAllEnemySpawn = currentAmoutOfEnemy1 + currentAmoutOfEnemy2 + currentAmoutOfEnemy3;
        }

        isChangeStage = false;
        stageUI.SetActive(false);

    }

    IEnumerator SpawnEnemy(GameObject enemy, Vector2 pos)
    {
        GameObject go = Instantiate(iconSpawner, pos, Quaternion.identity);
        GameObject spawnObj = Instantiate(enemy, go.transform.position, Quaternion.identity);
        spawnObj.SetActive(false);
        spriteRenderer = spawnObj.GetComponent<SpriteRenderer>();
        sortingOrder++;
        spriteRenderer.sortingOrder = sortingOrder;
        enemies.Add(spawnObj);
        yield return new WaitForSeconds(1.5f);
        spawnObj.SetActive(true);
        Destroy(go);
    }


    void RecieveItem()
    {
        items.AddRange(GameObject.FindGameObjectsWithTag("Item"));
        for (int i = 0;i < items.Count;i++)
        {
            items[i].transform.SendMessage("CanMove");
        }
        items.Clear();
    }

    void EnemyAllDeath()
    {
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        attackDetail.damage = 10000;
        attackDetail.attackDir = transform;
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].transform.SendMessage("Damage", attackDetail);
            if (enemies[i] == null)
            {
                enemies.Remove(enemies[i]);
            }
        }
        enemies.Clear();

    }

    Vector2 GetPos()
    {
        pos = new Vector2(Random.Range(spawnPoint[0].position.x, spawnPoint[2].position.x), Random.Range(spawnPoint[1].position.y, spawnPoint[3].position.y));
        return pos;
    }

}
