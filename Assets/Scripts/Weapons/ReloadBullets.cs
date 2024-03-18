using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBullets : MonoBehaviour
{
    //[SerializeField] private WeaponObject weaponsData;
    Weapon weapon;

    public int amountOfBullet {  get; private set; }

    private float currentReloadTimer;

    [SerializeField] private StatsBullet statsBullet;

    void Start()
    {
        weapon = CoinManager.Instance.GetWeaponSelected();
        statsBullet.weaponIcon.sprite = weapon.image;
        amountOfBullet = weapon.bullet;
        currentReloadTimer = weapon.reload;
        statsBullet.amountOfBulletText.text = weapon.bullet.ToString();
        statsBullet.reloadImage.fillAmount = 0;
    }
    private void Update()
    {
        Reload();
        //if(amountOfBullet > 0)
        //{
        //    statsBullet.reloadImage.fillAmount = 0;
        //}       
        statsBullet.amountOfBulletText.text = amountOfBullet.ToString();
    }

    public void UpdateBullets()
    {
        amountOfBullet--;
    }



    void Reload()
    {
        if (amountOfBullet <= 0)
        {
            currentReloadTimer -= Time.deltaTime;
            statsBullet.reloadImage.fillAmount = currentReloadTimer / weapon.reload;
            if (currentReloadTimer <= 0)
            {
                amountOfBullet = weapon.bullet;
                statsBullet.amountOfBulletText.text = amountOfBullet.ToString();
                currentReloadTimer = weapon.reload;
                statsBullet.reloadImage.fillAmount = 0;
            }
        }
    }
}
