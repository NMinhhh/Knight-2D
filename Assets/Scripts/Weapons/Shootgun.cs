using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootgun : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float timeLife;
    [SerializeField] private Transform[] attackPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float coolDown;
    private float time;
 
    // Update is called once per frame
    void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        time += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && time >= coolDown)
        {
            time = 0;
            SpawnBullet(attackPoint[0], attackPoint[0].rotation);
            SpawnBullet(attackPoint[1], attackPoint[1].rotation);
            SpawnBullet(attackPoint[2], attackPoint[2].rotation);
        }
    }

    void SpawnBullet(Transform spawnPos, Quaternion ro)
    {
        GameObject projectile = Instantiate(this.bullet, spawnPos.position, ro);
        Projectile script = projectile.GetComponent<Projectile>();
        script.CreateBullet(damage, speed, timeLife);
    }
}
