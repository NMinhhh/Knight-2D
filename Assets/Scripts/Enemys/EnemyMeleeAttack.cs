using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radiusAttack;
    [SerializeField] private float damage;
    [SerializeField] private float cooldownAttack;
    private float timeAttack;
    private AttackDetail attackDetail;
    private bool isFinishAnimation;
    private bool isAttack;

    private Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        timeAttack += Time.deltaTime;
        if (isFinishAnimation)
        {
            enemy.anim.SetBool("attack", false);
            isFinishAnimation = false;
        }
        else if (enemy.PlayerDetected() && !isFinishAnimation && timeAttack >= cooldownAttack)
        {
            timeAttack = 0;
            enemy.anim.SetBool("attack", true);
        }

    }

    void TriggerAnimation()
    {
        attackDetail.damage = this.damage;
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, radiusAttack, enemy.player);
        foreach (Collider2D col in hit)
        {
            if (col)
            {
                Debug.Log("Damage");
                //col.transform.SendMessage("Damage", attackDetail);
            }
        }
    }

    void FinishAnimation()
    {
        isFinishAnimation = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radiusAttack);

    }
}
