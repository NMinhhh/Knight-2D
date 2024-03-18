using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [Header("Data")]
    //[SerializeField] private WeaponObject weaponData;
    Weapon weapon;

    [Header("Shoting")]
    [SerializeField] private Transform[] attackPoint;
    private HandleRotation handleRotation;


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
        weapon = CoinManager.Instance.GetWeaponSelected();
        reloadBullets = GetComponent<ReloadBullets>();
        anim = transform.Find("MuzzleFlash").GetComponent<Animator>();
        handleRotation = GameObject.FindObjectOfType<HandleRotation>();
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (!handleRotation.auto && InputManager.Instance.shoting)
        {
            Shooting();
        }else if (handleRotation.auto && handleRotation.nearestObj != null)
        {
            Shooting();
        }
    }

    void Shooting()
    {
        if (timer >= weapon.cooldown && reloadBullets.amountOfBullet > 0)
        {
            timer = 0;
            anim.SetTrigger("shoot");
            SoundFXManager.Instance.CreateAudioClip(clip, attackPoint[0], .5f);
            if (gun)
            {
                ShootingGun();
            }
            else if (shotGun)
            {
                ShootingShotGun();
            }
            else
            {
                ShootingBomb();
            }
            reloadBullets.UpdateBullets();
        }
    }
    
    void ShootingGun()
    {
       
        SpawnBullet(attackPoint[0], attackPoint[0].rotation);
           
    }

    void ShootingShotGun()
    {
        foreach (Transform pos in attackPoint)
        {
            SpawnBullet(pos, pos.rotation);
        }
    }

    void ShootingBomb()
    {
        SpawnRocket(attackPoint[0], attackPoint[0].rotation);
          

    }

    void SpawnBullet(Transform spawnPos, Quaternion ro)
    {
        GameObject projectile = Instantiate(weapon.bulletIcon, spawnPos.position, ro);
        Projectile script = projectile.GetComponent<Projectile>();
        script.CreateBullet(weapon.damage, weapon.speed, weapon.timeLife);
    }

    void SpawnRocket(Transform spawnPos, Quaternion ro)
    {
        GameObject projectile = Instantiate(weapon.bulletIcon, spawnPos.position, ro);
        ProjectileBomb script = projectile.GetComponent<ProjectileBomb>();
        script.CreateBomb(weapon.damage, weapon.speed, weapon.timeLife);
    }
}
