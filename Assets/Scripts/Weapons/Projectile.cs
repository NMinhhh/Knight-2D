using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeLife;
    [SerializeField] private LayerMask whatIsEnemy;
    private Rigidbody2D rb;
    [SerializeField]private BoxCollider2D boxCollider;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        DestroyProj();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Attack();

    }
    void Attack()
    {
        Collider2D[] hit = Physics2D.OverlapBoxAll(transform.position, boxCollider.bounds.size, 0, whatIsEnemy);
        foreach (Collider2D hit2 in hit)
        {
            if (hit2)
            {
                Debug.Log("damge");
                Destroy(gameObject);
            }
        }
    }
    void DestroyProj()
    {
        Destroy(gameObject,timeLife);
    }

    
}
