using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager Instance {  get; private set; }

    [SerializeField] private BoxCollider2D boxCollider;

    [Header("SpawnEnemy")]
    [SerializeField] private GameObject iconSpawner;
    [SerializeField] private Transform enemyHolder;
    SpriteRenderer spriteRenderer;
    [Space]

    private static int sortingOrder = 0;

    private List<GameObject> enemyCheck = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();

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

    public int GetListEnemyCount()
    {
        return enemies.Count;
    }

    public void GenerateEnemy(Stage stage)
    {
        enemies.Clear();
        for (int i = 0; i < stage.GetEnemyObjLength(); i++)
        {
            EnemyObj enemyObj = stage.GetEnemyObj(i);
            for (int j = 0; j < enemyObj.amountOfEnemy; j++)
            {
                GameObject go = Instantiate(enemyObj.GetEnemyRan());
                go.SetActive(false);
                go.transform.parent = enemyHolder;
                enemies.Add(go);
            }
        }
        int n = enemies.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            GameObject temp = enemies[i];
            enemies[i] = enemies[j];
            enemies[j] = temp;
        }
    }

    public void AppearanceEnemy(int idx, float delayTimer)
    {
        StartCoroutine(CreateEnemyObj(idx, delayTimer));
    }

    IEnumerator CreateEnemyObj(int idx, float delayTimer)
    {
        Vector3 pos = GetRandomSpawnPosition();
        GameObject go = Instantiate(iconSpawner, pos, Quaternion.identity);
        GameObject enemy = enemies[idx];
        enemy.transform.position = pos; 
        spriteRenderer = enemy.GetComponent<SpriteRenderer>();
        sortingOrder++;
        spriteRenderer.sortingOrder = sortingOrder;
        enemyCheck.Add(enemy);
        yield return new WaitForSeconds(delayTimer);
        Destroy(go);
        enemy.SetActive(true);
    }

    public bool CheckListEnemy()
    {
        for (int i = 0; i < enemyCheck.Count; i++)
        {
            if (enemyCheck[i] == null)
            {
                enemyCheck.RemoveAt(i);
            }
        }
        return enemyCheck.Count == 0;
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