using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float timeLife;
    private Player player;
    private Vector2 mousePos;
    private Vector3 direction;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleGunRotation();
        Shooting();
    }

   

    void HandleGunRotation()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - (Vector2)gun.transform.position).normalized;
        gun.transform.right = direction;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector3 localScale = Vector3.one;

        if(angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = 1f;
        }

        if(player.isFacingRight)
        {
            localScale.x = 1f;
        }
        else
        {
            localScale.x = -1f;
        }
        gun.transform.localScale = localScale;
    }

    void Shooting()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject projectile = Instantiate(this.projectile, attackPoint.position, gun.transform.rotation);
            Projectile script = projectile.GetComponent<Projectile>();
            script.CreateBullet(damage, speed, timeLife);
        }
    }

}
