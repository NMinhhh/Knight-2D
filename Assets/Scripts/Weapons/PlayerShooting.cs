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
    [SerializeField] private int maxBullet;
    private int amountOfBullet;
    [SerializeField] private float reloadTimer;
    private float currentReloadTimer;
    [SerializeField] private StatsBullet statsBullet;


    [Header("Sound")]
    [SerializeField] private AudioClip clip;

    private HandleRotation handleRotation;

    private void Start()
    {
        handleRotation = GetComponent<HandleRotation>();
        amountOfBullet = maxBullet;
        currentReloadTimer = reloadTimer;
        statsBullet.amountOfBulletText.text = maxBullet.ToString();
        statsBullet.reloadImage.fillAmount = 0;
    }

    void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        timer += Time.deltaTime;
        if(InputManager.Instance.shoting && timer >= cooldownTimer && amountOfBullet > 0)
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
            amountOfBullet--;
            statsBullet.amountOfBulletText.text = amountOfBullet.ToString();
        }if(amountOfBullet <= 0)
        {
            currentReloadTimer -= Time.deltaTime;
            statsBullet.reloadImage.fillAmount = currentReloadTimer / reloadTimer;
            if(currentReloadTimer <= 0)
            {
                ReloadBullets();
            }
        }
    }

    void ReloadBullets()
    {
        amountOfBullet = maxBullet;
        statsBullet.amountOfBulletText.text = amountOfBullet.ToString();
        currentReloadTimer = reloadTimer;
        statsBullet.reloadImage.fillAmount = 0;
    }

}
