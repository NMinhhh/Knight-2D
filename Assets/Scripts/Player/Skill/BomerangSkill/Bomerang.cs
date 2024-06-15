using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomerang : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private GameObject light2D;

    [Header("Rotation value")]
    [SerializeField] private float valueRotationZ;
    private float rotationZ;
    [Space]

    [Range(0,1)]
    [SerializeField] private float distance;

    [SerializeField] private float radius;
    private float damage;
    private float speed;
    private float timeLife;

    [SerializeField] private LayerMask whatISEnemy;
    [SerializeField] private LayerMask whatIsWall;

    private Rigidbody2D rb;

    private Transform playerPos;

    AttackDetail attackDetail;

    void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
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

        timeLife -= Time.deltaTime;
        if(timeLife > 0)
        {
            if (CheckWall())
            {
                timeLife = 0;
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, playerPos.position) <= distance)
            {
                Destroy(gameObject);
            }
        }
        Attack();
    }

    void FixedUpdate()
    {
        RotationZ();
    }

    public void RotationZ()
    {
        rotationZ -= valueRotationZ;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        if (rotationZ <= -360)
        {
            rotationZ = 0;
        }

    }

    bool CheckWall()
    {
        return Physics2D.OverlapCircle(transform.position, radius, whatIsWall);
    }

    void Attack()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, radius, whatISEnemy);
        attackDetail.attackDir = transform;
        attackDetail.damage = damage;
        attackDetail.continousDamage = true;
        foreach (Collider2D col in hit)
        {
            if (col)
            {
                col.transform.SendMessage("Damage", attackDetail);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void CreateObj(float damage, float speed, float timeLife)
    {
        this.damage = damage;
        this.speed = speed;
        this.timeLife = timeLife;
    }

}
