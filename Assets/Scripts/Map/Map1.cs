using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1 : MonoBehaviour
{
    [Header("Map Level")]
    [SerializeField] private int mapLevel;
    [Space]

    [Header("Bonus")]
    [SerializeField] private int coinWin;
    [SerializeField] private int diamondWin;
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
    private bool isLastStage;
    [Space]
    [Space]


    [Header("Boss")]
    [SerializeField] private float warningTime;
    private float timer;
    private bool isBoss;


    private List<GameObject> items;

    [SerializeField] private int amountAllEnemySpawn;

    void Start()
    {
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
            else if (isLastStage)
            {
                if (SpawnerManager.Instance.CheckListEnemy())
                {
                    RecieveItem();
                    if (!isWin)
                    {
                        isWin = true;
                        MapManager.Instance.Winner();
                    }
                    endStageTimer -= Time.deltaTime;
                    if (endStageTimer <= 0)
                    {
                        endStageTimer = cooldownEndStageTimer;
                        MapWinner();
                    }
                }
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
            if (timer >= warningTime)
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


    void MapWinner()
    {
        if (!GameManager.Instance.GetMapState())
        {
            GameManager.Instance.SetMapState(true);
            GameManager.Instance.AddMapWin(mapLevel - 1);
            GameManager.Instance.AddMapUnlock(mapLevel);
            GameData.Instance.SetMapData();
            MapManager.Instance.PickUpCoin(coinWin);
            MapManager.Instance.PickUpDiamond(diamondWin);
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
        currentStage = stageData.GetStage(i);
        SpawnerManager.Instance.GenerateEnemy(currentStage);
        MapManager.Instance.SetBossState(currentStage.isBoss);
        MapManager.Instance.OpenOption();

    }

    void ChangeState()
    {
        if (currentStage.isBoss)
        {
            isBoss = true;
        }
        else
        {
            isBoss = false;
        }
        if (MapManager.Instance.stage == stageData.GetStageLength())
        {
            isLastStage = true;
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

}
