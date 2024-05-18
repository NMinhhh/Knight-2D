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
        
    }
}
