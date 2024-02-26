using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private WeaponObject data;

    [Header("Shoting")]
    [SerializeField] private Transform[] attackPoint;

    [Header("Cooldown")]
    private float timer;

    [Header("Reload")]
    private ReloadBullets reloadBullets;

    private Animator anim;

    [Header("Sound")]
    [SerializeField] private AudioClip clip;


    [Header("Type Of Weapon")]
    [SerializeField] private bool gun;
    [SerializeField] private bool shotGun;
    [SerializeField] private bool grenadeLauncher;

    private void Start()
    {
        reloadBullets = GetComponent<ReloadBullets>();
        anim = transform.Find("MuzzleFlash").GetComponent<Animator>();
    }

    void Update()
    {
        if(gun)
            ShootingGun();
        else if(shotGun)
            ShootingShotGun();
        else
            ShootingBomb();
    }

    void ShootingGun()
    {
        timer += Time.deltaTime;
        if(InputManager.Instance.shoting && timer >= data.cooldown && reloadBullets.amountOfBullet > 0)
        {
            timer = 0; 
            Vector3 localScale = Vector3.one;
            anim.SetTrigger("shoot");
            SoundFXManager.Instance.CreateAudioClip(clip, attackPoint[0], .5f);
            SpawnBullet(attackPoint[0], attackPoint[0].rotation);
            //GameObject GO = Instantiate(data.bulletIcon, attackPoint[0].position, attackPoint[0].rotation);
            //if (handleRotation.angle > 90 || handleRotation.angle < -90)
            //{
            //    localScale.y = -1f;
            //}
            //else
            //{
            //    localScale.y = 1f;
            //}
            //GO.transform.localScale = localScale;
            //Projectile script = GO.GetComponent<Projectile>();
            //script.CreateBullet(data.damage, data.speed, data.timeLife);
            reloadBullets.UpdateBullets();  
        }
    }

    void ShootingShotGun()
    {
        timer += Time.deltaTime;
        if (InputManager.Instance.shoting && timer >= data.cooldown && reloadBullets.amountOfBullet > 0)
        {
            timer = 0;
            anim.SetTrigger("shoot");
            SoundFXManager.Instance.CreateAudioClip(clip, attackPoint[0], .5f);
            foreach (Transform pos in attackPoint)
            {
                SpawnBullet(pos, pos.rotation);
            }
            reloadBullets.UpdateBullets();
        }

    }

    void ShootingBomb()
    {
        timer += Time.deltaTime;
        if (InputManager.Instance.shoting && timer >= data.cooldown && reloadBullets.amountOfBullet > 0)
        {
            timer = 0;
            anim.SetTrigger("shoot");
            SoundFXManager.Instance.CreateAudioClip(clip, attackPoint[0], .5f);
            SpawnRocket(attackPoint[0], attackPoint[0].rotation);
            reloadBullets.UpdateBullets();
        }

    }

    void SpawnBullet(Transform spawnPos, Quaternion ro)
    {
        GameObject projectile = Instantiate(data.bulletIcon, spawnPos.position, ro);
        Projectile script = projectile.GetComponent<Projectile>();
        script.CreateBullet(data.damage, data.speed, data.timeLife);
    }

    void SpawnRocket(Transform spawnPos, Quaternion ro)
    {
        GameObject projectile = Instantiate(data.bulletIcon, spawnPos.position, ro);
        ProjectileBomb script = projectile.GetComponent<ProjectileBomb>();
        script.CreateBomb(data.damage, data.speed, data.timeLife);
    }
}
