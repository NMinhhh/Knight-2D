using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGun : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private WeaponObject data;

    [Header("Shoting")]
    [SerializeField] private Transform attackPoint;
  
    [Header("Cooldown")]
    private float time;

    [Header("ReloadBullet")]
    private ReloadBullets reloadBullets;


    [SerializeField] private HandleRotation handleRotation;
    private void Start()
    {
        reloadBullets = GetComponent<ReloadBullets>();
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        time += Time.deltaTime;
        if (InputManager.Instance.shoting && time >= data.cooldown && reloadBullets.amountOfBullet > 0)
        {
            time = 0;
            Vector3 localScale = Vector3.one;
            GameObject GO = Instantiate(data.bulletIcon, attackPoint.position, transform.rotation);
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
            script.CreateBomb(data.damage, data.speed, data.timeLife);
            reloadBullets.UpdateBullets();
        }
    }
}
