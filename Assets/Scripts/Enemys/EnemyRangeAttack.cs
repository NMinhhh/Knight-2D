using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float damage;
    [SerializeField] private float speedBullet;
    [SerializeField] private float timeLife;
    [SerializeField] private float cooldownAttack;
    private float timeAttack;
    private bool isFininshAnimation;

    private Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        timeAttack = cooldownAttack;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        timeAttack += Time.deltaTime;
        if (isFininshAnimation)
        {
            enemy.anim.SetBool("attack", false);
            isFininshAnimation = false;
        }
        else if(enemy.PlayerDetected() && !isFininshAnimation && timeAttack >= cooldownAttack)
        {
            enemy.anim.SetBool("attack", true);
        }
    }

    Vector2 GetDir()
    {
        return (enemy.target.position - attackPoint.position).normalized;
    }

    void TriggerAnimation()
    {
        attackPoint.right = GetDir();
        GameObject GO = Instantiate(bullet, attackPoint.position, attackPoint.rotation);
        Projectile script = GO.GetComponent<Projectile>();
        script.CreateBullet(damage, speedBullet, timeLife);
    }


    void FinishAnimation()
    {
        isFininshAnimation = true;
        timeAttack = 0;
    }
}
