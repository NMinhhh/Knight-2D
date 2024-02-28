using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollection : MonoBehaviour
{
    [Header("AmountHealth")]
    [SerializeField] private float amountHealth;

    [Header("Rotation speed and cooldown")]
    [SerializeField] private float speedFlip;
    [SerializeField] private float cooldownFlip;
    private float timer;

    [Header("Check to healing")]
    [SerializeField] private Transform checkPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask whatIsPlayer;

    float rotationY;
    bool isFlip;
    // Start is called before the first frame update
    void Start()
    {
        timer = cooldownFlip;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Flip();
        CheckToHealing();
    }
    
    void CheckToHealing()
    {
        Collider2D hit = Physics2D.OverlapCircle(checkPoint.position, radius, whatIsPlayer);
        if (hit)
        {
            hit.gameObject.GetComponent<Player>().Healing(amountHealth);
            Destroy(gameObject);
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
