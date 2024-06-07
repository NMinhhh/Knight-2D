using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SkillText : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private float speed;
    private float timeLife;
    private Rigidbody2D rb;

    [SerializeField] private Vector2 sizecheck;
    private AttackDetail attackDetail;

    [SerializeField] private float radius;
    private List<GameObject> enemys = new List<GameObject>();

    private bool isDamage;

    private LineRenderer lineRenderer;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private int amountOfEnemy;
    [SerializeField] private float appear;
    private float timer;

    private bool isNext;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
        attackDetail.attackDir = transform;
        rb.velocity = transform.right * speed;
    }
    private void Update()
    {
        attackDetail.attackDir = transform;
        attackDetail.continousDamage = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isDamage)
        {
            Attack();
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= appear)
            {
                AttackNextEnemy();
                timer = 0;
            }
        }
    }



    void Draw2dRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    void AttackNextEnemy()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, radius, whatIsEnemy);
        GameObject goRan = null;
        if(enemys.Count < amountOfEnemy)
        {
            foreach(Collider2D col in hit)
            {
                if (!enemys.Contains(col.gameObject))
                {
                    goRan = col.gameObject;
                }
            }
        }else if(enemys.Count >= amountOfEnemy || goRan == null)
        {
            Destroy(gameObject);
            return;
        }
        enemys.Add(goRan);
        transform.position = goRan.transform.position;
        Draw2dRay(enemys[enemys.Count - 2].transform.position, goRan.transform.position);
    }

    void Attack()
    {
        Collider2D enemy = Physics2D.OverlapBox(transform.position, sizecheck, transform.eulerAngles.z, whatIsEnemy);
        if (enemy)
        {
            isDamage = true;
            rb.velocity = Vector2.zero;
            spriteRenderer.enabled = false;
            enemys.Add(enemy.gameObject);
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, sizecheck);
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
