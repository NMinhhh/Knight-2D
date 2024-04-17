using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExCollection : MonoBehaviour
{
    [SerializeField] private float ex;
    [SerializeField] private Vector2 speed;
    [SerializeField] private CircleCollider2D collider2d;
    [SerializeField] private LayerMask whatIsPlayer;
    private float distance = 5;
    private Transform player;
    private Rigidbody2D rb;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(player.position, transform.position) < distance)
        {
            rb.velocity = GetDir() * speed;
            speed += speed * Time.deltaTime;
        }
        CheckPlayer();
    }

    void CheckPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(collider2d.bounds.center, collider2d.radius, whatIsPlayer);
        if(hit)
        {
            GameManager.Instance.UpdateEx(ex);
            Destroy(gameObject);
        }
    }

    Vector2 GetDir()
    {
        return (player.position - transform.position).normalized;
    }
}
