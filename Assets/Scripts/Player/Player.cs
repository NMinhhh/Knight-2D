using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    public bool isFacingRight {  get; private set; }
    private float facingRight;
    private Animator anim;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isFacingRight = true;
        facingRight = 1;
    }

    // Update is called once per frame
    void Update()
    {
        InputMovement();
        CheckFlip();
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
