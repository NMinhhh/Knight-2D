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
    private List<GameObject> listBoss;
    private int encreaseTimeSpawn;
    private bool isBoss;
    private float time;
    private float startTime;
    private void Start()
    {
        time = Random.Range(cooldown.x,cooldown.y);
        startTime = Time.time;
        startTimerSpawnBoss += startTime;
        encreaseTimeSpawn = 1;
        listBoss = new List<GameObject>();
    }

    private void Update()
    {
        time -= Time.deltaTime;
        startTime += Time.deltaTime;
        if(startTime >= startTimerSpawn && time <= 0)
        {
            time = Random.Range(cooldown.x, cooldown.y) * encreaseTimeSpawn;
            SpawnerManager.Instance.SpawnEnemy(enemyNor[Random.Range(0, enemyNor.Length)], new Vector2(Random.Range(-16, 26), Random.Range(-17f, 13.5f)));
        }
        if(startTime >= startTimerSpawnBoss && !isBoss && GameManager.Instance.minutes == 1)
        {
            isBoss = true;
            encreaseTimeSpawn = 2;
            GameObject go = Instantiate(boss, new Vector2(Random.Range(-16, 26), Random.Range(-17f, 13.5f)),Quaternion.identity);
            listBoss.Add(go);
        }
        
       
    }
}
