using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamodCollection : MonoBehaviour
{
    private Vector3 destinationPoint;
    [SerializeField] private float speed;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private LayerMask whatIsPlayer;

    private Transform player;

    private bool canMove;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        destinationPoint = CanvasManager.Instance.GetDestinationDiamondPoint();
        CheckPlayer();
        Move();
    }

    void Move()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationPoint, speed * Time.deltaTime);
            if(Vector3.Distance(transform.position, destinationPoint) <= 0.1f)
            {
                transform.position = destinationPoint;
                GameManager.Instance.PickUpDiamond(1);
                Destroy(gameObject);
            }
        }
    }

    void CheckPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(circleCollider.bounds.center, circleCollider.radius, whatIsPlayer);
        if (hit)
        {
            spriteRenderer.sortingLayerName = "UI";
            anim.SetTrigger("diamond");
        }
    }

    void Trigger()
    {
        canMove = true;
    }

    public void CanMove()
    {
        canMove = true;
    }
}
