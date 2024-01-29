using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shoting")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float timeLife;
    [Header("Cooldown")]
    [SerializeField] private float cooldownTimer;
    private float timer;

    [Header("Reload")]
    private ReloadBullets reloadBullets;


    [Header("Sound")]
    [SerializeField] private AudioClip clip;

    private HandleRotation handleRotation;

    private void Start()
    {
        handleRotation = GetComponent<HandleRotation>();
        reloadBullets = GetComponent<ReloadBullets>();
    }

    void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        timer += Time.deltaTime;
        if(InputManager.Instance.shoting && timer >= cooldownTimer && reloadBullets.amountOfBullet > 0)
        {
            timer = 0; 

            Vector3 localScale = Vector3.one;
            SoundFXManager.Instance.CreateAudioClip(clip, attackPoint, .5f);
            GameObject GO = Instantiate(this.projectile, attackPoint.position, attackPoint.rotation);
            if (handleRotation.angle > 90 || handleRotation.angle < -90)
            {
                localScale.y = -1f;
            }
            else
            {
                localScale.y = 1f;
            }
            GO.transform.localScale = localScale;
            Projectile script = GO.GetComponent<Projectile>();
            script.CreateBullet(damage, speed, timeLife);
            reloadBullets.UpdateBullets();  
        }
    }
}
