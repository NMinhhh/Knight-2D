using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    [Header("Position spawn enemy")]
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private List<Vector2> sizeSpawnPoint;

    Vector2[] pos;

    [Header("Warning UI")]
    [SerializeField] private GameObject warningBoss;
    [SerializeField] private float timeWarningMax;
    private float timeWarningCur;

    [Header("Enemy")]
    [SerializeField] private GameObject[] enemyNor;
    [SerializeField] private GameObject[] enemyMed;
    [SerializeField] private GameObject boss;
    //Cooldown Spawn
    public float startTimerSpawn;
    private List<GameObject> listBoss;


    //index cooldown Spawn
    private bool isBoss;

    // time start spawn enemy
    private float time;
    private float timeCheck;

    private void Start()
    {
        //time = Random.Range(cooldown.x,cooldown.y);
        time = startTimerSpawn;
        listBoss = new List<GameObject>();
        timeWarningCur = timeWarningMax;
        timeCheck = 15;
        pos = new Vector2[4];
    }

    private void Update()
    {
        time -= Time.deltaTime;
        timeCheck -= Time.deltaTime;
        if(timeCheck <= 0 && startTimerSpawn > 0.1f)
        {
            timeCheck = 15;
            startTimerSpawn -= startTimerSpawn * 10 / 100; 
        }


        if (time <= 0)
        {
            time = startTimerSpawn;
            if( startTimerSpawn > .8f)
                SpawnerManager.Instance.SpawnItem(enemyNor[Random.Range(0, enemyNor.Length)], GetPos());
            else
                SpawnerManager.Instance.SpawnItem(enemyMed[Random.Range(0, enemyMed.Length)], GetPos());
        }

        if(!isBoss && GameManager.Instance.minutes == 10)
        {
            warningBoss.SetActive(true);
            timeWarningCur -= Time.deltaTime;
            if(timeWarningCur < 0)
            {
                warningBoss.SetActive(false);
                isBoss = true;
                timeWarningCur = timeWarningMax;
                GameObject go = Instantiate(boss, new Vector2(Random.Range(-16, 26), Random.Range(-17f, 13.5f)), Quaternion.identity);
                listBoss.Add(go);
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
}
