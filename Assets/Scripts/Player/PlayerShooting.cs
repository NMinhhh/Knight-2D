using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float timeLife;

    // Update is called once per frame
    void Update()
    {
        //transform.position = GameObject.Find("Handle Weapons").transform.position;
        Shooting();
    }

    void Shooting()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject projectile = Instantiate(this.projectile, attackPoint.position, transform.rotation);
            Projectile script = projectile.GetComponent<Projectile>();
            script.CreateBullet(damage, speed, timeLife);
        }
    }

}
