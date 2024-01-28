using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGun : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float timeLife;
    [SerializeField] private float coolDown;
    private float time;

    // Update is called once per frame
    void Update()
    {
        //transform.position = GameObject.Find("Handle Weapons").transform.position;
        Shooting();
    }

    void Shooting()
    {
        time += Time.deltaTime;
        if (InputManager.Instance.shoting && time >= coolDown)
        {
            time = 0;
            GameObject GO = Instantiate(this.projectile, attackPoint.position, transform.rotation);
            ProjectileBomb script = GO.GetComponent<ProjectileBomb>();
            script.CreateBomb(damage, speed, timeLife);
        }
    }
}
