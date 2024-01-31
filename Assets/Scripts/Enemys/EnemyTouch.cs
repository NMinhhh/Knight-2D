using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTouch : MonoBehaviour
{
    [SerializeField] private Transform touchPoint;
    [SerializeField] private float damage;
    [SerializeField] private Vector2 size;
    [SerializeField] private float cooldown;
    private float time;

    private AttackDetail attackDetail;

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
        time += Time.deltaTime;
        attackDetail.attackDir = transform;
        attackDetail.damage = this.damage;
        Collider2D[] hit = Physics2D.OverlapBoxAll(touchPoint.position, size, 0, enemy.player);
        foreach (Collider2D col in hit)
        {
            if (col && time >= cooldown)
            {
                col.transform.SendMessage("Damage", attackDetail);
                time = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(touchPoint.position, size); 
    }
}
