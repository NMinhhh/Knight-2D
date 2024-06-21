using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private GameObject[] light2Ds;
    [Space]

    [Header("Info")]
    [SerializeField] private Transform checkPoint;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private float radiusDamage;
    protected float speed;
    protected float timeLife;
    private bool isDamage;
    [Space]

    [Header("Layer")]
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private LayerMask whatIsDeathZone;

    protected AttackDetail attackDetail;

    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attackDetail.attackDir = transform;
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (DayNightCircle.Instance.isNight)
        {
            foreach(GameObject light2D in light2Ds)
            {
                light2D.SetActive(true);

            }
        }
        else
        {
            foreach (GameObject light2D in light2Ds)
            {
                light2D.SetActive(false);

            }
        }

        timeLife -= Time.deltaTime;
        if (isDamage)
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("explode", true);
        }
        else if(CheckDeathZone() || timeLife <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void FixedUpdate()
    {
        CheckEnemy();
    }

    void CheckEnemy()
    {
        Collider2D enemy = Physics2D.OverlapCircle(checkPoint.position, radius, whatIsEnemy);
        attackDetail.attackDir = transform;
        if (enemy && !isDamage)
        {
            isDamage = true;
        }
     
    }

    bool CheckDeathZone()
    {
        return Physics2D.OverlapCircle(checkPoint.position, radius, whatIsDeathZone);
    }

    protected virtual void Attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.position, radiusDamage, whatIsEnemy);
        attackDetail.continousDamage = false;
        foreach(Collider2D col in enemy)
        {
            if(col)
            {
                col.transform.SendMessage("Damage", attackDetail);
            }
        }
      
    }

    public void CreateBomb(float damage, float speed, float timeLife)
    {
        attackDetail.damage = damage;
        this.speed = speed;
        this.timeLife = timeLife;
    }

    void PlaySound()
    {
        SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.RocketExplosion);
    }

    void DestroyGO()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkPoint.position, radius);
        Gizmos.DrawWireSphere(attackPoint.position, radiusDamage);
    }
}
