using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class ExplosiveBullet : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private GameObject light2D;
    [Space]

    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    protected float speed;
    protected float timeLife;
    protected AttackDetail attackDetail;

    private bool isDamage;

    private Animator anim;
    private Rigidbody2D rb;

    [Header("Spawn bullet after explode")]
    [SerializeField] private GameObject bulletObj;
    private GameObject go;
    [SerializeField] private int amountOfBullet;
    private PenetratingBullet script;
    private GameObject enemyCol;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attackDetail.attackDir = transform;
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        if (DayNightCircle.Instance.isNight)
        {
            light2D.SetActive(true);
        }
        else
        {
            light2D.SetActive(false);
        }

        if (!isDamage)
            Attack();
    }

    protected virtual void Attack()
    {
        Collider2D enemy = Physics2D.OverlapCircle(attackPoint.position, radius, whatIsEnemy);
        attackDetail.continousDamage = false;
        if (enemy)
        {
            rb.velocity = Vector2.zero;
            isDamage = true;
            enemy.transform.SendMessage("Damage", attackDetail);
            anim.SetBool("explode", true);
            enemyCol = enemy.gameObject;
        }

    }

    public void SpawnBulllet()
    {
        float rotationz;
        Vector3 direction;
        int amountOfEnemy = 0;
        GameObject enemyRam;
        Collider2D[] enemys = GetPositionInCam.Instance.GetEnemysPosition();

        List<GameObject> listEnemy = new List<GameObject>();

        foreach (Collider2D col in enemys)
        {
            listEnemy.Add(col.gameObject);
        }

        if (listEnemy.Contains(enemyCol))
        {
            listEnemy.RemoveAt(listEnemy.IndexOf(enemyCol));
        }

        for (int i = 0; i < amountOfBullet; i++)
        {
            if (enemys.Length > 0 && amountOfEnemy < enemys.Length && listEnemy.Count > 0)
            {
                amountOfEnemy++;
                enemyRam = listEnemy[Random.Range(0, listEnemy.Count)];
                direction = (enemyRam.transform.position - transform.position).normalized;
                rotationz = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            }
            else
            {
                rotationz = Random.Range(0, 360);
            }
            go = Instantiate(bulletObj, transform.position, Quaternion.Euler(0, 0, rotationz));
            script = go.GetComponent<PenetratingBullet>();
            script.AddEnemyCol(enemyCol);
            script.CreateBullet(attackDetail.damage, speed, timeLife);
        }
    }

    public void CreateBullet(float damage, float speed, float timeLife)
    {
        attackDetail.damage = damage;
        this.speed = speed;
        this.timeLife = timeLife;
    }

    public void DestroyGO()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }

  

}
