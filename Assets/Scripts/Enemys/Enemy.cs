using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth;
    private float currentHealth;
    [Space]
    [Space]

    [Header("Move")]
    [SerializeField] private float speed;
    private Transform target;
    private bool isMove;
    [Space]
    [Space]

    [Header("Hurt")]
    [SerializeField] private float hurtTime;
    [SerializeField] private float knockbackSpeed;


    [Header("Attack")]
    [SerializeField] private Transform checkPlayer;
    [SerializeField] private Vector2 sizeCheckPlayer;
    [SerializeField] private LayerMask whatIsPlayer;
    public LayerMask player { get; private set; }

    //Other Variable
    private float facingRight;

    //Components
    public Animator anim {  get; private set; }
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        player = whatIsPlayer;
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
        if (PlayerDetected())
        {
            isMove = false;
            rb.velocity = Vector2.zero;
        }
        else if(!PlayerDetected() && !anim.GetBool("attack"))
        {
            isMove = true;
            rb.velocity = GetDir() * speed;
        }
        anim.SetBool("move", isMove);
    }


    public bool PlayerDetected()
    {
        return Physics2D.OverlapBox(checkPlayer.position, sizeCheckPlayer, 0, whatIsPlayer);
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
        if (!anim.GetBool("attack"))
        {
            if (target.position.x < transform.position.x && facingRight == 1)
            {
                Flip();
            }
            else if (target.position.x > transform.position.x && facingRight == -1)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(checkPlayer.position, sizeCheckPlayer);
    }
}
