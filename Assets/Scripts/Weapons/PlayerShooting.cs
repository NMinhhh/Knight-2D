    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public enum GunType
    {
        NormalGun,
        ShotingGun,
        PenetratingGun,
        RocketGun,
        BulletExplodeGun,
        LaserGun
    }

    [Header("Type of gun")]
    [SerializeField] private GunType gunType;
    [Space]


    [Header("Shoting")]
    [SerializeField] private Transform[] attackPoint;
    [Space]

    [Header("Cooldown")]
    private float timer;
    [Space]

    [Header("Reload")]
    private ReloadBullets reloadBullets;
    [Space]

    [Header("Sound")]
    [SerializeField] private AudioClip clip;
    [Space]

    [Header("Muzzle Flash")]
    [SerializeField] private bool isMuzzleFlash;

    private Animator anim;

    Weapon weapon;

    private float currentDamage;

    private void Start()
    {
        reloadBullets = GetComponent<ReloadBullets>();
        if(isMuzzleFlash)
            anim = transform.Find("MuzzleFlash").GetComponent<Animator>();
        ResetWeaponDamage();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (AutoShoot.isAutoShoot)
        {
            Shooting();
        }

    }

    public void IncreaseDamage(float amount)
    {
        ResetWeaponDamage();
        currentDamage += currentDamage * amount / 100;
    }

    public void ResetWeaponDamage()
    {
        if(weapon == null)
            weapon = GameManager.Instance.GetWeaponSelected();
        currentDamage = weapon.damage;
    }

    void Shooting()
    {
        if (timer >= weapon.cooldown && reloadBullets.amountOfBullet > 0)
        {
            timer = 0;
            if(anim)
                anim.SetTrigger("shoot");
            SoundFXManager.Instance.CreateAudioClip(clip, attackPoint[0], .5f);
            Shooting(gunType);
            reloadBullets.UpdateBullets();
        }
    }
    
    void Shooting(GunType type)
    {
        switch(type)
        {
            case GunType.NormalGun:
                NormalGunShoot();
                break;
            case GunType.ShotingGun:
                ShotingGunShoot();
                break;
            case GunType.PenetratingGun:
                PenetratingGunShoot();
                break;
            case GunType.RocketGun:
                RoketGunShoot();
                break; 
            case GunType.BulletExplodeGun:
                BulletExplodeGunShoot();
                break; 
            case GunType.LaserGun:
                LaserGunShoot();
                break;
        }
    }

   //===========================Normal Gun==========================

    void NormalGunShoot()
    {
        SpawnNormalBullet(attackPoint[0], attackPoint[0].rotation);
    }

    void SpawnNormalBullet(Transform spawnPos, Quaternion ro)
    {
        GameObject normalBullet = Instantiate(weapon.bulletIcon, spawnPos.position, ro);
        NormalBullet script = normalBullet.GetComponent<NormalBullet>();
        script.CreateBullet(this.currentDamage, weapon.speed, weapon.timeLife);
    }

    //===========================Shoting Gun===========================

    void ShotingGunShoot()
    {
        foreach (Transform pos in attackPoint)
        {
            SpawnNormalBullet(pos, pos.rotation);
        }
    }

    //================================Rocket Gun============================

    void RoketGunShoot()
    {
        SpawnRocket(attackPoint[0], attackPoint[0].rotation);
    }

    void SpawnRocket(Transform spawnPos, Quaternion ro)
    {
        GameObject rocket = Instantiate(weapon.bulletIcon, spawnPos.position, ro);
        Rocket script = rocket.GetComponent<Rocket>();
        script.CreateBomb(this.currentDamage, weapon.speed, weapon.timeLife);
    }
    

    //===============================BulletExplodeGun================================

    void BulletExplodeGunShoot()
    {
        SpawnExplosiveBbullet(attackPoint[0], attackPoint[0].rotation);
    }

    void SpawnExplosiveBbullet(Transform spawnPos, Quaternion ro)
    {
        GameObject explosiveBullet = Instantiate(weapon.bulletIcon, spawnPos.position, ro);
        ExplosiveBullet script = explosiveBullet.GetComponent<ExplosiveBullet>();
        script.CreateBomb(this.currentDamage, weapon.speed, weapon.timeLife);
    }

    //===============================Penetratig Gun===============================

    void PenetratingGunShoot()
    {
        SpawnPenetratingBullet(attackPoint[0], attackPoint[0].rotation);
    }

    void SpawnPenetratingBullet(Transform spawnPos, Quaternion ro)
    {
        GameObject penetratingBullet = Instantiate(weapon.bulletIcon, spawnPos.position, ro);
        Meteor script = penetratingBullet.GetComponent<Meteor>();
        script.CreateMeteor(this.currentDamage, weapon.speed, weapon.timeLife);
    }

    //===================================Laser Gun============================

    void LaserGunShoot()
    {
        LaserBullet script = transform.GetChild(0).GetComponent<LaserBullet>();
        script.CreateLaser(currentDamage);
        script.OnLaser();
        AutoShoot.SetIsLockHandle(true);
    }

   
}
