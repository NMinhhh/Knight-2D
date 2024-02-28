using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager Instance {  get; private set; }

    [Header("SpawnEnemy")]
    [SerializeField] private GameObject iconSpawner;
    private GameObject GO;
    [Space]

    [Header("Spawn Item")]
    [SerializeField] private GameObject[] item;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnEnemy(GameObject enemy, Vector2 pos)
    {
        StartCoroutine(Spawn(enemy, pos));
    }

    IEnumerator Spawn(GameObject enemy, Vector2 pos)
    {
        GameObject go = Instantiate(iconSpawner, pos, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        GO = Instantiate(enemy, go.transform.position, Quaternion.identity);
        Destroy(go);
    }

    public void SpawnItem(GameObject item, Vector2 pos)
    {
        GameObject go = Instantiate(item, pos, Quaternion.identity);
    }

    public GameObject GetItem(int i) 
    {
        return item[i];
    }
}