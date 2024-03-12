using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBlastSpawnSkill : MonoBehaviour
{
    [SerializeField] private GameObject spawnGo;
    private GameObject go;
    private WaterBlast script;
    [SerializeField] private float cooldown;
    [SerializeField] private int maxDir;
    [SerializeField] private float damage;
    [SerializeField] private Vector2 speed;
    [SerializeField] private float time;
    private int currentDir;
    private float timer;
    void Start()
    {
        timer = cooldown;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        timer -= Time.deltaTime;
        if (timer <= 0 && currentDir > 0)
        {
            timer = cooldown;
            SetSkill(currentDir);
        }
    }

    public void AddDirSkill(int i)
    {
        currentDir = i;
    }

    void SetSkill(int level)
    {
        Vector3 pos;
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < level; i++)
        {
            if (enemy.Length > 0)
            {
                pos = enemy[Random.Range(0, enemy.Length)].transform.position;
            }
            else
            {
                pos = new Vector3(Random.Range(transform.position.x + 10, transform.position.x - 10), Random.Range(transform.position.y + 10, transform.position.y - 10), 0);
            }
            go = Instantiate(spawnGo, transform.position, Quaternion.identity);
            script = go.GetComponent<WaterBlast>();
            script.SetSkill(damage, Random.Range(speed.x, speed.y), time, pos);
        }
    }
}
