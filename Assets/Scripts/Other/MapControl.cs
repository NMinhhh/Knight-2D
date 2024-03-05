using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    [Header("Position spawn enemy")]
    [SerializeField] private Transform player;
    [SerializeField] private Vector2 sizeX;
    [SerializeField] private Vector2 sizeY;
    Vector2[] pos;

    [Header("Warning UI")]
    [SerializeField] private GameObject warningBoss;
    [SerializeField] private float timeWarningMax;
    private float timeWarningCur;

    [Header("Enemy")]
    [SerializeField] private GameObject[] enemyNor;
    [SerializeField] private GameObject boss;
    //Cooldown Spawn
    [SerializeField] private Vector2 cooldown;
    public float startTimerSpawn;
    private List<GameObject> listBoss;


    //index cooldown Spawn
    private bool isBoss;

    // time start spawn enemy
    private float time;
    private float startTime;
    private float timeCheck;

    private void Start()
    {
        //time = Random.Range(cooldown.x,cooldown.y);
        startTimerSpawn = cooldown.x;
        time = startTimerSpawn;
        listBoss = new List<GameObject>();
        timeWarningCur = timeWarningMax;
        timeCheck = 15;
        pos = new Vector2[4];
    }

    private void Update()
    {
        time -= Time.deltaTime;
        startTime = GameManager.Instance.minutes;
        timeCheck -= Time.deltaTime;
        if(timeCheck <= 0 && startTimerSpawn > 0)
        {
            timeCheck = 15;
            startTimerSpawn -= startTimerSpawn * 10 / 100; 
        }


        if (time <= 0)
        {
            time = startTimerSpawn;
            SpawnerManager.Instance.SpawnItem(enemyNor[Random.Range(0, enemyNor.Length)], GetPos());
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

            pos[0] = new Vector2(player.position.x + Random.Range(-15, 15), player.position.y + sizeY.x);
            pos[1] = new Vector2(player.position.x + Random.Range(-15, 15), player.position.y + sizeY.y);
            pos[2] = new Vector2(player.position.x + sizeX.x, player.position.y + Random.Range(-9, 9));
            pos[3] = new Vector2(player.position.x + sizeX.y, player.position.y + Random.Range(-9, 9));
            return pos[Random.Range(0, pos.Length)];
       }
    }
}
