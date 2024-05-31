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

    [Header("Stage Data")]
    [SerializeField] private StageData stageData;
    private Stage currentStage;

    private int enemyIndex;
    private bool isFinishStage;
    private float spawnCooldown;

    private bool isWin;

    [Header("Stage UI")]
    [SerializeField] private StageUI stageUI;
    [SerializeField] private float cooldownstartStageTimer;
    private float startStageTimer;
    [SerializeField] private float cooldownEndStageTimer;
    private float endStageTimer;
    private bool isChangeStage;
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

    void Start()
    {
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
                        SetUpStage();
                        startStageTimer = Time.time;
                        isChangeStage = true;
                        if (isWin) 
                            CheckToWin();
                        else
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


    void CheckToWin()
    {
        Debug.Log("you win");
        if (!GameManager.Instance.GetMapState())
        {
            GameManager.Instance.AddMapWin(mapLevel - 1);
            GameManager.Instance.AddMapUnlock(mapLevel);
        }
        GameStateUI.Instance.OpenWinUI();
    }

    void ControlSpawn()
    {
       
        spawnCooldown -= Time.deltaTime;
        if (spawnCooldown <= 0)
        {
            SpawnerManager.Instance.AppearanceEnemy(enemyIndex, 1.5f);
            spawnCooldown = currentStage.cooldown;
            enemyIndex++;
            if (enemyIndex == SpawnerManager.Instance.GetListEnemyCount())
            {
                isFinishStage = true;
            }
        }


    }

    void SetUpStage()
    {
        MapManager.Instance.StageLevelUp();
        int i = MapManager.Instance.stage - 1;
        if(i == stageData.GetStageLength())
        {
            isWin = true;
            return;
        }
        currentStage = stageData.GetStage(i);
        SpawnerManager.Instance.GenerateEnemy(currentStage);
    }

    void ChangeState()
    {
        if (currentStage.isBoss)
        {
            isBoss = true;
        }
        isChangeStage = false;
        isFinishStage = false;
        enemyIndex = 0;
        spawnCooldown = 0;
        stageUI.CloseStageUI();
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
