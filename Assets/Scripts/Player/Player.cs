using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Vector2 movement;

    [Header("Health")]
    [SerializeField] private float maxHealth;
    private float currentHelth;
    [SerializeField] private float hurtTime;
    [SerializeField] private float imortalTime;
    [SerializeField] private float numberOfFlash;
    private bool isImortal;
    public bool isDie {  get; private set; }
    [Space]
    [Space]
    [SerializeField] private GameObject floatingText;
    public bool isFacingRight {  get; private set; }
    private float facingRight;
    private int damageDir;

    private PlayerStats stats;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        stats = GetComponent<PlayerStats>();
        isFacingRight = true;
        facingRight = 1;
        currentHelth = maxHealth;
    }

    void Update()
    {
        Movement();
        CheckFlip();
    }

    public void Healing(float amout)
    {
        currentHelth = Mathf.Clamp(currentHelth + amout, 0, maxHealth);
        stats.UpdateHealth(currentHelth, maxHealth);
    }

    void Damage(AttackDetail attackDetail)
    {
        if (isImortal || isDie) return;
        currentHelth = Mathf.Clamp(currentHelth - attackDetail.damage, 0, maxHealth);
        stats.UpdateHealth(currentHelth, maxHealth);
        if (attackDetail.attackDir.position.x > transform.position.x)
        {
            damageDir = -1;
        }
        else
        {
            damageDir = 1;
        }
        FloatingTextManager.Instance.CreateFloatingText(floatingText, transform.position, attackDetail.damage.ToString(), damageDir);
        if (currentHelth > 0)
        {
            StartCoroutine(Hurt());
        }
        else
        {
            GameManager.Instance.GameStats();
            isDie = true;
        }
    }

    IEnumerator Hurt()
    {
        sprite.color = new Color(.95f, .55f, .55f , 1);
        yield return new WaitForSeconds(hurtTime);
        sprite.color = Color.white;
    }

    IEnumerator Imortal()
    {
        isImortal = true;
        for (int i = 0; i < numberOfFlash; i++)
        {
            sprite.color = new Color(.95f, .55f, .55f, 1);
            yield return new WaitForSeconds(imortalTime / (numberOfFlash * 2));
            sprite.color = Color.white;
            yield return new WaitForSeconds(imortalTime / (numberOfFlash * 2));
        }
        isImortal = false;
    }

    public void Born()
    {
        isDie = false;
        StartCoroutine(Imortal());
        currentHelth = maxHealth;
        stats.UpdateHealth(maxHealth, maxHealth);
        GameManager.Instance.GameStats();

    }

    void Movement()
    {
        movement.Set(InputManager.Instance.xInput, InputManager.Instance.yInput);
        rb.velocity = movement * movementSpeed;
        anim.SetBool("move", movement.x != 0 || movement.y != 0);
    }

    void CheckFlip()
    {
        if(Input.GetAxisRaw("Horizontal") < 0 && isFacingRight)
        { 
            Flip();
        }else if(Input.GetAxisRaw("Horizontal") > 0 && !isFacingRight) {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        facingRight *= -1;
        transform.localScale = new Vector3(facingRight, 1, 1);
    }

  
}
