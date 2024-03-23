using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Map1 : MonoBehaviour
{

    [Header("Position spawn enemy")]
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private List<Vector2> sizeSpawnPoint;

    Vector2[] pos;
    [Space]
    [Space]

    [Header("Enemy")]
    [SerializeField] private GameObject[] enemyNor;
    [SerializeField] private GameObject[] enemyMed;
    [Space]

    //cooldown spawn enemy
    [SerializeField] private float cooldown;
    private float maxCooldown;
    private float timeCur;
    [SerializeField] private float timeChecktoDecreaseCooldown;
    private float timeCheckCur;
    //amount rate enemy
    private int amountOfENor;


    //Time in game
    private int minute;

    void Start()
    {
        pos = new Vector2[4];
        timeCur = cooldown;
        maxCooldown = cooldown;
        timeCheckCur = timeChecktoDecreaseCooldown * 60;
    }

    // Update is called once per frame
    void Update()
    {
        minute = GameManager.Instance.minutes;
        timeCur -= Time.deltaTime;
        if(timeCur < 0)
        {
            int i = Random.Range(0,enemyNor.Length);
            if(minute < 1)
            {
                Spawn(enemyNor[i], 1);
            }
            else if(minute < 3)
            {
                Spawn(enemyNor[i], 2);
            }else if(minute < 5)
            {
                Spawn(enemyNor[i], 3);
                Spawn(enemyMed[0], 1);
            }else if(minute < 7)
            {
                Spawn(enemyNor[i], 4);
                Spawn(enemyMed[0], 1);
            }else if(minute < 8)
            {
                Spawn(enemyNor[i], 5);
                Spawn(enemyMed[0], 2);
            }
            else if(minute < 13)
            {
                Spawn(enemyNor[i], 6);
                Spawn(enemyMed[0], 3);
            }
            else
            {
                Spawn(enemyNor[i], 7);
                Spawn(enemyMed[0], 5);
            }
            timeCur = cooldown;
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
