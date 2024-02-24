using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyNor;
    [SerializeField] private GameObject boss;
    [SerializeField] private float startTimerSpawn;
    [SerializeField] private float startTimerSpawnBoss;
    [SerializeField] private Vector2 cooldown;
    private bool isBoss;
    private float time;
    private float startTime;
    private void Start()
    {
        time = Random.Range(cooldown.x,cooldown.y);
        startTime = Time.time;
        startTimerSpawnBoss += startTime;
    }

    private void Update()
    {
        time -= Time.deltaTime;
        startTime += Time.deltaTime;
        if(startTime >= startTimerSpawn && time <= 0)
        {
            time = Random.Range(cooldown.x, cooldown.y);
            SpawnerManager.Instance.SpawnEnemy(enemyNor[Random.Range(0, enemyNor.Length - 1)], new Vector2(Random.Range(-16, 26), Random.Range(-17f, 13.5f)));
        }
        if(startTime >= startTimerSpawnBoss && !isBoss)
        {
            isBoss = true;
            SpawnerManager.Instance.SpawnEnemy(boss, new Vector2(Random.Range(-16, 26), Random.Range(-17f, 13.5f)));
        }
    }
}
