using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager Instance {  get; private set; }

    [SerializeField] private BoxCollider2D boxCollider;

    [Header("SpawnEnemy")]
    [SerializeField] private GameObject iconSpawner;
    SpriteRenderer spriteRenderer;
    [Space]

    private static int sortingOrder = 0;

    private List<GameObject> enemies = new List<GameObject>();

    [Header("Energy pref")]
    [SerializeField] private GameObject energy;

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

    public void SpawnEnemy(GameObject enemy, float delayTimer)
    {
        StartCoroutine(Spawn(enemy, delayTimer));
    }

    IEnumerator Spawn(GameObject enemy, float delayTimer)
    {
        GameObject go = Instantiate(iconSpawner, GetRandomSpawnPosition(), Quaternion.identity);
        GameObject spawnGo = Instantiate(enemy, go.transform.position, Quaternion.identity);
        spawnGo.SetActive(false);
        spriteRenderer = spawnGo.GetComponent<SpriteRenderer>();
        sortingOrder++;
        spriteRenderer.sortingOrder = sortingOrder;
        enemies.Add(spawnGo);    
        yield return new WaitForSeconds(delayTimer);
        spawnGo.SetActive(true);
        Destroy(go);
    }

    public bool CheckListEnemy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }
        return enemies.Count == 0;
    }

    public void SpawnEnergy(Vector2 pos)
    {
        Instantiate(energy, pos, Quaternion.identity);
    }


    public Vector2 GetRandomSpawnPosition()
    {
        Vector2 minBound = new Vector2(boxCollider.bounds.min.x, boxCollider.bounds.min.y);
        Vector2 maxBound = new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.max.y);

        float ranX = Random.Range(minBound.x, maxBound.x);
        float ranY = Random.Range(minBound.y, maxBound.y);

        return new Vector2(ranX, ranY);
    }
}