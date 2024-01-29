using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBullets : MonoBehaviour
{
    [SerializeField] private int maxBullet;
    public int amountOfBullet {  get; private set; }
    [SerializeField] private float reloadTimer;
    private float currentReloadTimer;
    [SerializeField] private StatsBullet statsBullet;
    bool isReload;

    void Start()
    {
        amountOfBullet = maxBullet;
        currentReloadTimer = reloadTimer;
        statsBullet.amountOfBulletText.text = maxBullet.ToString();
        statsBullet.reloadImage.fillAmount = 0;
    }
    private void Update()
    {
        Reload();
        if(amountOfBullet > 0)
        {
            statsBullet.reloadImage.fillAmount = 0;
        }       
        statsBullet.amountOfBulletText.text = amountOfBullet.ToString();
    }

    public void UpdateBullets()
    {
        amountOfBullet--;
    }



    void Reload()
    {
        if(amountOfBullet <= 0)
        {
            currentReloadTimer -= Time.deltaTime;
            statsBullet.reloadImage.fillAmount = currentReloadTimer / reloadTimer;
            if (currentReloadTimer <= 0)
            {
                amountOfBullet = maxBullet;
                statsBullet.amountOfBulletText.text = amountOfBullet.ToString();
                currentReloadTimer = reloadTimer;
                statsBullet.reloadImage.fillAmount = 0;
            }
        }
    }
}
