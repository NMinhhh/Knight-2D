using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject spawnGO;
    [SerializeField] private float cooldownSpawnTime;
    [SerializeField] private GameObject spawnerIcon;
    [SerializeField] private Vector2 spawnTimer;
    [SerializeField] private int amountOfEnemy;

    public List<GameObject> spawnList;
    public int count;
    private int currentAmount;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        currentAmount = amountOfEnemy;
        StartCoroutine(Spawner());
        timer = Random.Range(spawnTimer.x, spawnTimer.y);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && currentAmount > 0)
        {
            timer = Random.Range(spawnTimer.x, spawnTimer.y);
            StartCoroutine(Spawner());
        }
        for(int i = 0; i < spawnList.Count; ++i)
        {
            if (spawnList[i] == null) 
            { 
                spawnList.RemoveAt(i);
            }
        }
        count = spawnList.Count;
        if(currentAmount <= 0 && spawnList.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Spawner()
    {
        GameObject spawn = Instantiate(spawnerIcon, new Vector2(Random.Range(12, 29), Random.Range(-9, 8)), Quaternion.identity);
        yield return new WaitForSeconds(cooldownSpawnTime);
        GameObject enemy = Instantiate(spawnGO, spawn.transform.position, Quaternion.identity);
        spawnList.Add(enemy);
        Destroy(spawn);
        currentAmount--;
    }
}
