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

    public bool isFacingRight {  get; private set; }
    private float facingRight;

    //Component
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        isFacingRight = true;
        facingRight = 1;
        currentHelth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        InputMovement();
        CheckFlip();
    }

    void Damage(AttackDetail attackDetail)
    {
        currentHelth = Mathf.Clamp(currentHelth - attackDetail.damage, 0, maxHealth);

        if(currentHelth > 0)
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

    void InputMovement()
    {
        float velocityX = Input.GetAxisRaw("Horizontal");
        float velocityY = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(velocityX * movementSpeed, velocityY * movementSpeed, 0);

        rb.velocity = moveDir;


        anim.SetBool("move", velocityX != 0 || velocityY != 0);
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
}
