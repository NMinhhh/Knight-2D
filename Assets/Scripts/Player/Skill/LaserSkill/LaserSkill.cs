using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSkill : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private GameObject[] laser;
    [SerializeField] private float cooldown;
    private float timer;
    Laser script;

    private int level;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > cooldown && level > 0)
        {
            SetSkill();
            timer = 0;
        }
    }

    public void LevelUp(int level)
    {
        this.level = level;
        script = laser[level - 1].GetComponent<Laser>();
        script.CreateLaser(damage);
    }

    void SetSkill()
    {
        float rotationz;
        Vector3 direction;
        int amountOfEnemy = 0;
        GameObject enemyRam = null;
        Collider2D[] enemys = EnemysPosition.Instance.GetEnemysPosition();
        for (int i = 0; i < level; i++)
        {
            if (enemys.Length > 0 && amountOfEnemy < enemys.Length)
            {
                amountOfEnemy++;
                enemyRam = enemys[i].gameObject;
                direction = (enemyRam.transform.position - laser[i].transform.position).normalized;
                rotationz = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            }
            else
            {
                enemyRam = null;
                rotationz = Random.Range(0, 360);
            }
            script = laser[i].GetComponent<Laser>();
            laser[i].transform.eulerAngles = new Vector3(0, 0, rotationz);
            if(enemyRam == null)
            {
                script.OnLaser();
            }
            else
            {
               script.OnLaser(enemyRam);
            }
        }
    }
}
