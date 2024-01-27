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
        if(InputManager.Instance.shoting)
        {
            GameObject GO = Instantiate(this.projectile, attackPoint.position, transform.rotation);
            Projectile script = GO.GetComponent<Projectile>();
            script.CreateBullet(damage, speed, timeLife);
        }
    }

}
