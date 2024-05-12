using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map2 : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyNor;
    [SerializeField] private GameObject[] enemyBig;
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private float cooldown;
    [SerializeField] private float amountOfEnemyNor;
    private float currentAmountOfEnemyNor;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        if(Time.time >= startTime + cooldown)
        {
            currentAmountOfEnemyNor += 1;
            Instantiate(enemyNor[Random.Range(0, enemyNor.Length)], spawnPoint[Random.Range(0,spawnPoint.Length)].position, Quaternion.identity);
        }
        if(currentAmountOfEnemyNor == amountOfEnemyNor && GameManager.Instance.level >= 15)
        {
            currentAmountOfEnemyNor = 0;
            Instantiate(enemyBig[Random.Range(0, enemyBig.Length)], spawnPoint[Random.Range(0, spawnPoint.Length)].position, Quaternion.identity);
        }
    }
}
