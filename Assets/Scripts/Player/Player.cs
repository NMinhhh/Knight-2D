using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    private float currenSpeed;
    private Vector2 movement;
    private int horizontal;
    private int vertical;

    [Header("Health")]
    [SerializeField] private float maxHealth;
    private float currentHelth;

    [Header("Hurt Timer")]
    [SerializeField] private float hurtTime;
    private bool isHurt;
    public bool isDie { get; private set; }

    [Header("Imortal")]
    [SerializeField] private float imortalTime;
    [SerializeField] private float numberOfFlash;
    private bool isImortal;

    [Space]
    [Space]
    [SerializeField] private GameObject floatingText;
    [SerializeField] private Color floatingTextColor;

    //Other Variable
    public bool isFacingRight {  get; private set; }
    private int damageDir;

    //Skill state
    private bool isProtection;


    private PlayerStats stats;

    //Componet
    private Rigidbody2D rb;
    private SpriteRenderer sprite;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        stats = GetComponent<PlayerStats>();
        isFacingRight = true;
        currentHelth = maxHealth;
        currenSpeed = movementSpeed;
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (InputManager.Instance.yInput != 0)
        {
            movement.Set(InputManager.Instance.xInput, InputManager.Instance.yInput);
        }
        else
        {
            movement.Set(0, 0);
        }
        rb.velocity = movement * currenSpeed;
    }

   
    public void ProtectionSkillOn()
    {
        isProtection = true;
    }

    public void ProtectionSkillOff()
    {
        isProtection = false;
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
        FloatingTextManager.Instance.CreateFloatingText(floatingText, transform.position, attackDetail.damage.ToString(), floatingTextColor, damageDir);
        if (currentHelth > 0)
        {
            isHurt = true;
        }
        else
        {
            isDie = true;
        }
        if (isHurt)
        {
            StartCoroutine(Imortal());
        }
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
        stats.UpdateHealth(0, maxHealth);

    }

    public void IncreaseSpeed(float amount)
    {
        currenSpeed += movementSpeed * amount / 100;
    }

    public void AddHealth(float amout)
    {
        currentHelth = maxHealth + amout;
    }

    public void ResetPlayer()
    {
        currentHelth = maxHealth;
        currenSpeed = movementSpeed;
        stats.UpdateHealth(currentHelth, maxHealth);

    }

}
