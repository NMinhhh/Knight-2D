using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [Header("Image lightning")]
    [SerializeField] private Sprite[] sprites;
    private int animationStep;
    private float timeChange;

    [Header("Particle and effect")]
    [SerializeField] private GameObject particle;
    [SerializeField] private GameObject effect;

    [Header("Damage radius")]
    [SerializeField] private float radius;
    [SerializeField] private LayerMask whatIsEnemy;

    [Header("size lightning")]
    [Range(0f, 3f)]
    [SerializeField] private float value;
    private float height;
    private float damage;

    AttackDetail attackDetail;
    Vector2 size;
    float distance;
    private Vector3 attackPoint;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        distance = Vector2.Distance(transform.position, attackPoint);
    }

    private void Update()
    {
        Animation();
        height += value;
        if (height < distance)
        {
            size = new Vector2(spriteRenderer.size.x, height);
        }
        else
        {
            Attack();
            Instantiate(particle, attackPoint, Quaternion.identity);
            Instantiate(effect, attackPoint, Quaternion.identity);
            Destroy(gameObject);
        }
        spriteRenderer.size = size;
    }

    public void Animation()
    {
        timeChange += Time.deltaTime;
        if(timeChange > .1f)
        {
            animationStep++;
            if(animationStep >= sprites.Length)
            {
                animationStep = 0;
            }
            spriteRenderer.sprite = sprites[animationStep];
            timeChange = 0;
        }
    }

    public void Set(Vector3 attackPoint,float damage)
    {
        this.attackPoint = attackPoint;
        this.damage = damage;
    }

    void Attack()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint, radius, whatIsEnemy);
        attackDetail.attackDir = transform;
        attackDetail.damage = damage;
        attackDetail.continousDamage = false;
        foreach (Collider2D col in hit)
        {
            if (col)
            {
                col.transform.SendMessage("Damage", attackDetail);
            }
        }
    }


    void FinishAnimation()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
