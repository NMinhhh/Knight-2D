using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1 : MonoBehaviour
{
    [Header("Map Level")]
    [SerializeField] private int mapLevel;
    [Space]
    [Space]

    [Header("Stage")]
    [SerializeField] private StageUI stageUI;
    [SerializeField] private float cooldownstartStageTimer;
    private float startStageTimer;
    [SerializeField] private float cooldownEndStageTimer;
    private float endStageTimer;
    private bool isChangeStage;
    [Space]
    [Space]

    [Header("Cooldown")]
    [SerializeField] private float cooldownEnemy1;
    private float startTime1;
    [Space]
    [Space]


    [Header("Boss")]
    [SerializeField] private float warningTime;
    private float timer;
    private bool isBoss;

    private List<GameObject> enemies;

    private List<GameObject> items;

    [SerializeField] private int amountAllEnemySpawn;

    AttackDetail attackDetail;

    [Header("Text")]
    [SerializeField] private StageData stageData;
    private Stage currentStage;
    private int currentAmountOfEnemy;
    private bool isFinishStage;
    private float currentTimer;
    public List<GameObject> enemys;

    void Start()
    {
        enemys = new List<GameObject>();
        startTime1 = Time.deltaTime;
        enemies = new List<GameObject>();
        items = new List<GameObject>();
        isFinishStage = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBoss)
        {
            if (!isFinishStage)
            {
                ControlSpawn();
            }
            else
            {
                if (SpawnerManager.Instance.CheckListEnemy() && !isChangeStage)
                {
                    RecieveItem();
                    endStageTimer -= Time.deltaTime;
                    if (endStageTimer <= 0)
                    {
                        startStageTimer = Time.time;
                        isChangeStage = true;
                        stageUI.OpenStageUI();
                        endStageTimer = cooldownEndStageTimer;
                    }

                }
            }
            if (Time.time >= startStageTimer + cooldownstartStageTimer && isChangeStage)
            {
                ChangeState();
            }
        }
        else
        {
            timer += Time.deltaTime;
            if(timer >= warningTime)
            {
                stageUI.CloseWarningBossUI();
                isBoss = false;
                timer = 0;
            }
            else
            {
                stageUI.OpenWarningBossUI();
            }

        }

    }


    void ControlSpawn()
    {
       
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            SpawnerManager.Instance.SpawnEnemy(enemys[currentAmountOfEnemy], 1.5f);
            currentTimer = currentStage.cooldown;
            currentAmountOfEnemy++;
            if (currentAmountOfEnemy == currentStage.enemyPref.Length)
            {
                isFinishStage = true;
            }
        }


    }

    void ChangeState()
    {
        int i = GameManager.Instance.stage - 1;
        currentStage = stageData.GetStage(i);
        ShuffleEnemy();
        if(currentStage.isBoss)
        {
            isBoss = true;
        }
        isChangeStage = false;
        isFinishStage = false;
        currentAmountOfEnemy = 0;
        currentTimer = 0;
        stageUI.CloseStageUI();

    }

    void ShuffleEnemy()
    {
        enemys.Clear();
        enemys.AddRange(currentStage.GetEnemy());
        int n = enemys.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            GameObject temp = enemys[i];
            enemys[i] = enemys[j];
            enemys[j] = temp;
        }
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

}
