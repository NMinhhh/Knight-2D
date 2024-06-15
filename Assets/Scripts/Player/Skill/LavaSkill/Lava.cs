using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private GameObject light2D;
    [SerializeField] private float radius;
    private float damage;
    private float timeLife;

    [SerializeField] private LayerMask whatIsEnemy;
    private float cooldown;
    private float timer;

    private Animator anim;

    AttackDetail attackDetail;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        attackDetail.attackDir = transform;
        Destroy(gameObject, timeLife);
        timer = cooldown;
    }

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

        timer += Time.deltaTime;
        if(timer >= cooldown)
        {
            Attack();
            timer = 0;
        }
    }

    public void CreateLava(float damage, float cooldown, float timeLife, float radius)
    {
        this.damage = damage;
        this.cooldown = cooldown;
        this.timeLife = timeLife;
        this.radius = radius;
    }

    void LavaAppear()
    {
        anim.SetBool("isLava", true);
    }

    void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, whatIsEnemy);
        attackDetail.damage = damage;
        attackDetail.attackDir = transform;
        attackDetail.continousDamage = false;

        foreach (Collider2D hit in hits)
        {
            if (hit)
            {
                hit.transform.SendMessage("Damage", attackDetail);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
