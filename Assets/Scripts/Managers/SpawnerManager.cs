using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager Instance {  get; private set; }

    [Header("SpawnEnemy")]
    [SerializeField] private GameObject iconSpawner;
    SpriteRenderer spriteRenderer;
    private GameObject GO;
    [Space]

    private static int sortingOrder = 0;

    [Header("Spawn Item")]
    [SerializeField] private GameObject[] item;

    [Header("Ex Item")]
    [SerializeField] private GameObject[] exItem;

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
        spriteRenderer = GO.GetComponent<SpriteRenderer>();
        sortingOrder++;
        spriteRenderer.sortingOrder = sortingOrder;
        yield return GO;
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

    public GameObject GetExItem(int i)
    {
        return exItem[i];
    }
}