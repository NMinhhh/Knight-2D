using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private GameObject light2D;
    [Space]

    [Header("Layer")]
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private LayerMask whatIsDeathZone;
    private float speed;
    private float timeLife;
    private Rigidbody2D rb;

    [SerializeField] private Vector2 sizecheck;
    private AttackDetail attackDetail;

    private bool isDamage;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        timeLife -= Time.deltaTime;
        if(isDamage || CheckDeathZone() || timeLife <= 0)
        {
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Attack();
    }

    void Attack()
    {
        Collider2D enemy = Physics2D.OverlapBox(transform.position, sizecheck, transform.eulerAngles.z, whatIsEnemy);
        attackDetail.attackDir = transform;
        attackDetail.continousDamage = false;
        if (enemy)
        {
            isDamage = true;
            enemy.transform.SendMessage("Damage", attackDetail);
        }
    }

    bool CheckDeathZone()
    {
        return Physics2D.OverlapBox(transform.position, sizecheck, transform.eulerAngles.z, whatIsDeathZone); ;
    }

    public void CreateBullet(float damage, float speed, float timeLife)
    {
        attackDetail.damage = damage;
        this.speed = speed;
        this.timeLife = timeLife;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, sizecheck);
    }
}
