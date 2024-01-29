using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGun : MonoBehaviour
{
    [Header("Shoting")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float timeLife;

    [Header("Cooldown")]
    [SerializeField] private float coolDown;
    private float time;

    [Header("ReloadBullet")]
    private ReloadBullets reloadBullets;


    private HandleRotation handleRotation;
    private void Start()
    {
        reloadBullets = GetComponent<ReloadBullets>();
        handleRotation = GetComponent<HandleRotation>();
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        time += Time.deltaTime;
        if (InputManager.Instance.shoting && time >= coolDown)
        {
            time = 0;
            Vector3 localScale = Vector3.one;
            GameObject GO = Instantiate(this.projectile, attackPoint.position, transform.rotation);
            if(handleRotation.angle > 90 && handleRotation.angle < -90)
            {
                localScale.y = -1;

            }
            else
            {
                localScale.y = 1;
            }
            GO.transform.localScale = localScale;
            ProjectileBomb script = GO.GetComponent<ProjectileBomb>();
            script.CreateBomb(damage, speed, timeLife);
            reloadBullets.UpdateBullets();
        }
    }
}
