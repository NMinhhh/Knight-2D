using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCollection : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private GameObject light2D;

    [Header("Rotation speed and cooldown")]
    [SerializeField] private float speedFlip;
    [SerializeField] private float cooldownFlip;
    private float timer;
    [Space]
    [Space]

    [Header("Check to healing")]
    [SerializeField] private Transform checkPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask whatIsPlayer;
    [Space]
    [Space]

    [Header("Speed")]
    [SerializeField] private float speed;
    private Vector3 destinationPoint;
    float rotationY;
    bool isFlip;

    private Transform player;

    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        timer = cooldownFlip;
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (DayNightCircle.Instance.isNight)
        {
            light2D.SetActive(true);
        }
        else
        {
            light2D.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        destinationPoint = CanvasManager.Instance.GetDestinationEnergyPoint();
        Flip();
        CheckPlayer();
        Move();
    }
    
    public void CanMove()
    {
        canMove = true;
    }

    void Move()
    {
        if (canMove)
        {
            speed += speed * Time.unscaledDeltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destinationPoint, speed * Time.unscaledDeltaTime);
            if (Vector3.Distance(transform.position, destinationPoint) <= 0.1f)
            {
                transform.position = destinationPoint;
                MapManager.Instance.AddEnergy();
                Destroy(gameObject);
            }
        }
    }

    void CheckPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(checkPoint.position, radius, whatIsPlayer);
        if (hit)
        {
            CanMove();
        }
    }

    void Flip()
    {
        if (isFlip)
        {
            rotationY += speedFlip;
            transform.rotation = Quaternion.Euler(0, rotationY, 0);
            if (rotationY == 180 || rotationY == 360)
            {
                isFlip = false;
                if (rotationY == 360)
                    rotationY = 0;
            }

        }
        else
        {
            CheckIfFlip();
        }
    }

    void CheckIfFlip()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            isFlip = true;
            timer = cooldownFlip;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkPoint.position, radius);
    }
}
