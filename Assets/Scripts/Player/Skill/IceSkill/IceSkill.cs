using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkill : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float radiusMax;
    private float radius;


    [SerializeField] private LayerMask whatIsEnemy;

    private SpriteRenderer spriteRenderer;
    private Animator anim;

    AttackDetail attackDetail;
    // Start is called before the first frame update
    void Start()
    {
        radius = radiusMax;
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void LevelUpSkill()
    {
        if(!anim.GetBool("isSkill"))
            anim.SetBool("isSkill", true);
        radius += radiusMax * 0.25f;
        spriteRenderer.transform.localScale = new Vector2(transform.localScale.x + 0.25f, transform.localScale.y + 0.25f);

    }

    public void Attack()
    {
        attackDetail.attackDir = transform;
        attackDetail.damage = damage;
        attackDetail.continousDamage = false;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, whatIsEnemy);
        foreach (Collider2D hit in hits)
        {
            if (hit)
            {
                hit.transform.SendMessage("Damage", attackDetail);
                hit.transform.SendMessage("IsSlowingEffect");
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelUpSkill();
        }
    }
}
