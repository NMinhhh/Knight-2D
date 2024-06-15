using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetratingBullet : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private GameObject light2D;
    [Space]

    [Header("Point Damage")]
    [SerializeField] private Transform checkPoint;
    [SerializeField] private float radius;
    [SerializeField] private GameObject particle;
    private float damage;
    private float speed;
    private float timeLife;
    [Space]
    [Space]

    [SerializeField] private int amountOfEnemyCol;
    private int currentAmountOfEnemyCol;

    private List<GameObject> listEnemyCol;

    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private LayerMask whatIsWall;

    private Rigidbody2D rb;

    AttackDetail attackDetail;

    private void Awake()
    {
        listEnemyCol = new List<GameObject>();
    }

    void Start()
    {
        attackDetail.attackDir = transform;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        currentAmountOfEnemyCol = amountOfEnemyCol;
    }

    // Update is called once per frame
    void Update()
    {
        if (DayNightCircle.Instance.isNight)
        {
            light2D.SetActive(true);
        }
        else
        {
            light2D.SetActive(false);
        }

        Attack();

        timeLife -= Time.deltaTime;
        if (timeLife <= 0 || currentAmountOfEnemyCol <= 0)
        {
            Destroy(gameObject);
        }
    }

    bool CheckWall()
    {
        return Physics2D.OverlapCircle(checkPoint.position, radius, whatIsWall);
    }

    public void CreateBullet(float damage, float speed, float timeLife)
    {
        this.damage = damage;
        this.speed = speed;
        this.timeLife = timeLife;
    }

    public void AddEnemyCol(GameObject enemyCol)
    {
        listEnemyCol.Add(enemyCol);
    }

    void Attack()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(checkPoint.position, radius, whatIsEnemy);
        attackDetail.damage = damage;
        attackDetail.attackDir = transform;
        attackDetail.continousDamage = false;
        foreach (Collider2D col in hit)
        {
            if(col && !listEnemyCol.Contains(col.gameObject))
            {
                currentAmountOfEnemyCol--;
                listEnemyCol.Add(col.gameObject);
                Instantiate(particle, col.transform.position, Quaternion.identity);
                col.transform.SendMessage("Damage", attackDetail);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkPoint.position, radius);
    }
}
