using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float speed;
    private Transform target;
    private float currentHealth;
    private float facingRight;
    private bool isMove;
    private Animator anim;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        currentHealth = maxHealth;
        facingRight = 1;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfFlip();
        Movement();
    }
    Vector2 GetDir()
    {
        return (target.position - transform.position).normalized;
    }
    void Movement()
    {
        if (Vector3.Distance(target.position, transform.position) <= 1f)
        {
            isMove = false;
            rb.velocity = Vector2.zero;
        }
        else
        {
            isMove = true;
            rb.velocity = GetDir() * speed;
        }
        anim.SetBool("move", isMove);
    }

    void AttackPlayer()
    {

    }

    void Damage(AttackDetail attackDetail)
    {
        currentHealth = Mathf.Clamp(currentHealth - attackDetail.damage, 0, maxHealth);

        if(currentHealth > 0)
        {
            Debug.Log("hurt");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void CheckIfFlip()
    {
        if(target.position.x < transform.position.x && facingRight == 1)
        {
            Flip();
        }
        else if(target.position.x > transform.position.x && facingRight == -1)
        {
            Flip();
        }
    }

    void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight *= -1;
    }
}
