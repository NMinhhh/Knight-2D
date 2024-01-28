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
    [SerializeField] private float cooldownTimer;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        //transform.position = GameObject.Find("Handle Weapons").transform.position;
        Shooting();
    }

    void Shooting()
    {
        timer += Time.deltaTime;
        if(InputManager.Instance.shoting && timer >= cooldownTimer)
        {
            timer = 0;
            GameObject GO = Instantiate(this.projectile, attackPoint.position, transform.rotation);
            Projectile script = GO.GetComponent<Projectile>();
            script.CreateBullet(damage, speed, timeLife);
        }
    }

}
