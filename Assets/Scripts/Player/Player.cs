using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    private Vector2 movement;

    [Header("Health")]
    //Health
    [SerializeField] private float maxHealth;
    private float currentHelth;
    //Hurt timer
    [SerializeField] private float hurtTime;
    //Imortal Timer
    [SerializeField] private float imortalTime;
    [SerializeField] private float numberOfFlash;
    private bool isImortal;

    public bool isDie {  get; private set; }
    [Space]
    [Space]
    [SerializeField] private GameObject floatingText;

    //Other Variable
    public bool isFacingRight {  get; private set; }
    private float facingRight;
    private int damageDir;

    //Skill state
    private bool isProtection;


    private PlayerStats stats;

    //Componet
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

    public void ProtectionSkillOn()
    {
        isProtection = true;
    }

    public void ProtectionSkillOff()
    {
        isProtection = false;
    }

    public void Healing(float amout)
    {
        currentHelth = Mathf.Clamp(currentHelth + amout, 0, maxHealth);
        stats.UpdateHealth(currentHelth, maxHealth);
    }

    void Damage(AttackDetail attackDetail)
    {
        if (isImortal || isDie || isProtection) return;
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
