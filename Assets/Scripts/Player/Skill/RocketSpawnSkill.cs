using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawnSkill : MonoBehaviour
{
    [SerializeField] private GameObject spawnGo;
    private GameObject go;
    private ProjectileBomb script;
    [SerializeField] private float cooldown;
    [SerializeField] private int maxDir;
    [SerializeField] private float damage;
    [SerializeField] private Vector2 speed;
    [SerializeField] private float time;
    private int currentDir;
    private int level;
    private float timer;
    void Start()
    {
        timer = cooldown;
        //level = 1;
        //currentDir = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameManager.Instance.level > level && currentDir < maxDir)
        //{

        //    level++;
        //    currentDir++;
        //}
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

    void SetSkill(int dir)
    {
        float rotationZ;
        Vector3 direction;
        GameObject enemyRam;
        GameObject[] pos = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < dir; i++) 
        {
            if(pos.Length > 0)
            {
                enemyRam = pos[Random.Range(0, pos.Length - 1)];
                direction = (enemyRam.transform.position - transform.position).normalized;
                rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            }
            else
            {
                rotationZ = Random.Range(0, 360);

            }
            go = Instantiate(spawnGo,transform.position,Quaternion.Euler(0,0,rotationZ));
            script = go.GetComponent<ProjectileBomb>();
            script.CreateBomb(damage, Random.Range(speed.x,speed.y), time);
        }
    }
}
