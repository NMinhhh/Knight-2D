using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private GameObject light2D;

    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    private float currenSpeed;
    private float speed;
    private Vector2 movement;

    [Header("Health")]
    [SerializeField] private float maxHealth;
    private float currentHelth;
    private float health;

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
    [SerializeField] private Transform handleWeapon;
    private SpriteRenderer weaponSpriteRenderer;

    //Other Variable
    public bool isFacingRight {  get; private set; }
    private int damageDir;

    //Skill state
    private bool isProtection;


    private PlayerStats stats;

    public Weapon weaponSelected { get; private set; }

    //Componet
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        stats = GetComponent<PlayerStats>();
        isFacingRight = true;
        IndexSetting();
        ResetPlayer();
    }

    void Update()
    {
        if (DayNightCircle.Instance.isNight)
        {
            light2D.SetActive(true);
        }
        else
        {
            light2D.SetActive(false);
        }
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
        if (isImortal || isDie || isProtection || MapManager.Instance.isWin) return;
        currentHelth = Mathf.Clamp(currentHelth - attackDetail.damage, 0, health);
        stats.UpdateHealth(currentHelth, health);
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
        GetSpriteWeapon();
        isImortal = true;
        currenSpeed = 12;
        for (int i = 0; i < numberOfFlash; i++)
        {
            sprite.color = new Color(.95f, .55f, .55f, 1);
            weaponSpriteRenderer.color = new Color(.95f, .55f, .55f, 1);
            yield return new WaitForSeconds(imortalTime / (numberOfFlash * 2));
            sprite.color = Color.white;
            weaponSpriteRenderer.color = Color.white;
            yield return new WaitForSeconds(imortalTime / (numberOfFlash * 2));
        }
        currenSpeed = speed;
        isImortal = false;
    }

    void GetSpriteWeapon()
    {
        if (weaponSpriteRenderer == null)
        {
            int count = handleWeapon.childCount;
            for(int i = 0; i < count; i++)
            {
                if (handleWeapon.GetChild(i).gameObject.activeInHierarchy)
                {
                    weaponSpriteRenderer = handleWeapon.GetChild(i).GetComponent<SpriteRenderer>();
                }
            }
        }
    }

    public void Born()
    {
        isDie = false;
        StartCoroutine(Imortal());
        currentHelth = health;
        stats.UpdateHealth(health, health);

    }

    public void IncreaseSpeed(float amount)
    {
        ResetPlayer();
        speed += movementSpeed * amount / 100;
        currenSpeed = speed;
    }

    public void AddHealth(float amout)
    {
        ResetPlayer();
        health += maxHealth * amout / 100;
        currentHelth = health;
    }

    public void IndexSetting()
    {
        weaponSelected = GameManager.Instance.GetWeaponSelected();
        maxHealth = weaponSelected.maxHealth;
        movementSpeed = weaponSelected.movementSpeed;
    }

    public void ResetPlayer()
    {
        health = Mathf.FloorToInt(maxHealth);
        speed = movementSpeed;
        currentHelth = health;
        currenSpeed = speed;
        stats.UpdateHealth(currentHelth, health);

    }

}
