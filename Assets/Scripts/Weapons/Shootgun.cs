using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootgun : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private WeaponObject data;
    [Header("Shoting")]
    [SerializeField] private Transform[] attackPoint;

    [Header("Cooldown")]
    private float time;

    [Header("Reload")]
    private ReloadBullets reloadBullets;

    [Header("Sound")]
    [SerializeField] private AudioClip clip;

    private Animator anim;

    private void Start()
    {
        reloadBullets = GetComponent<ReloadBullets>();
        anim = transform.Find("MuzzleFlash").GetComponent<Animator>();
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
            anim.SetTrigger("shoot");
            SoundFXManager.Instance.CreateAudioClip(clip, attackPoint[0], .5f);
            foreach(Transform pos in attackPoint)
            {
                SpawnBullet(pos, pos.rotation);
            }
            reloadBullets.UpdateBullets();
        }
        
    }

    void SpawnBullet(Transform spawnPos, Quaternion ro)
    {
        GameObject projectile = Instantiate(data.bulletIcon, spawnPos.position, ro);
        Projectile script = projectile.GetComponent<Projectile>();
        script.CreateBullet(data.damage, data.speed, data.timeLife);
    }

   
}
