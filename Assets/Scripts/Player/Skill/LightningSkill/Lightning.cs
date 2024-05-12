using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject particle;
    [SerializeField] private GameObject effect;

    private Vector3 attackPoint;

    [SerializeField] private float radius;
    [SerializeField] private LayerMask whatIsEnemy;
    [Range(0f, 3f)]
    [SerializeField] private float value;
    private float height;
    private float damage;
    AttackDetail attackDetail;
    Vector2 localScale;
    float distance;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        distance = Vector2.Distance(transform.position, attackPoint);
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    private void Update()
    {
        height += value;
        if (height < distance)
        {
            localScale = new Vector2(spriteRenderer.size.x, height);
        }
        else
        {
            Attack();
            Instantiate(particle, attackPoint, Quaternion.identity);
            Instantiate(effect, attackPoint, Quaternion.identity);
            Destroy(gameObject);
        }
        spriteRenderer.size = localScale;
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
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
