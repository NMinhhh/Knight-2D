using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawnSkill : MonoBehaviour
{
    //Rocket Ogj
    [SerializeField] private GameObject spawnGo;
    private GameObject go;
    private ProjectileBomb script;
    //Cool down
    [SerializeField] private float cooldown;
    private float timer;

    //Dir spawn Obj
    private int currentDir;

    //Info Rocket
    [SerializeField] private float damage;
    [SerializeField] private Vector2 speed;
    [SerializeField] private float timeLife;

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

    void SetSkill(int dir)
    {
        float rotationZ;
        Vector3 direction;
        GameObject enemyRam;
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < dir; i++) 
        {
            if(enemy.Length > 0)
            {
                enemyRam = enemy[Random.Range(0, enemy.Length)];
                direction = (enemyRam.transform.position - transform.position).normalized;
                rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            }
            else
            {
                rotationZ = Random.Range(0, 360);

            }
            go = Instantiate(spawnGo,transform.position,Quaternion.Euler(0,0,rotationZ));
            script = go.GetComponent<ProjectileBomb>();
            script.CreateBomb(damage, Random.Range(speed.x,speed.y), timeLife);
        }
    }
}
