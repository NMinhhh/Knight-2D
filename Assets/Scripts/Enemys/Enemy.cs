using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private float hurtTime;
    [SerializeField] private float knockbackSpeed;
    private Transform target;
    private float currentHealth;
    private float facingRight;
    private bool isMove;
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
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
            StartCoroutine(Hurt());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Hurt()
    {
        sprite.color = new Color(.95f,.6f , .6f, 1);
        rb.velocity = Vector2.one;
        rb.velocity = GetDir() * -knockbackSpeed;
        yield return new WaitForSeconds(hurtTime);
        sprite.color = new Color(1, 1, 1, 1);
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
