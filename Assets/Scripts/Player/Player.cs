using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    [Header("Health")]
    [SerializeField] private float maxHealth;
    private float currentHelth;
    [SerializeField] private float hurtTime;
    [Space]
    [Space]
    [SerializeField] private GameObject floatingText;
    public bool isFacingRight {  get; private set; }
    private float facingRight;
    private int damageDir;

    private PlayerStats stats;


    //Component
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    [Header("Text")]
    [SerializeField] private GameObject[] guns;
    private int amountOfGuns;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        stats = GetComponent<PlayerStats>();
        isFacingRight = true;
        facingRight = 1;
        currentHelth = maxHealth;
        guns[amountOfGuns].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckFlip();
        TextCode();
    }

    void Damage(AttackDetail attackDetail)
    {
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
        FloatingTextManager.Instance.CreateFloatingText(floatingText, transform, attackDetail.damage.ToString(), damageDir);
        if (currentHelth > 0)
        {
            StartCoroutine(Hurt());
        }
        else
        {
            //Die
        }
    }

    IEnumerator Hurt()
    {
        sprite.color = new Color(.95f, .55f, .55f , 1);
        yield return new WaitForSeconds(hurtTime);
        sprite.color = new Color(1, 1, 1, 1);
    }

    void Movement()
    {
        float xInput = InputManager.Instance.xInput;
        float yInput = InputManager.Instance.yInput;
        Vector3 moveDir = new Vector3(xInput * movementSpeed, yInput * movementSpeed, 0);

        rb.velocity = moveDir;


        anim.SetBool("move", xInput != 0 || yInput != 0);
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
        facingRight *= -1;
        transform.localScale = new Vector3(facingRight, 1, 1);
        isFacingRight = !isFacingRight;
    }

    void TextCode()
    {
        if (Input.GetMouseButtonDown(1))
        {
            guns[amountOfGuns].SetActive(false);
            amountOfGuns++;
            if (amountOfGuns >= guns.Length)
                amountOfGuns = 0;
            guns[amountOfGuns].SetActive(true);
        }
    }
}
