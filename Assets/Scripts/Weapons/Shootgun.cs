using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootgun : MonoBehaviour
{
    [Header("Shoting")]
    [SerializeField] private Transform[] attackPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float timeLife;

    [Header("Cooldown")]
    [SerializeField] private float coolDown;
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
        if (InputManager.Instance.shoting && time >= coolDown && reloadBullets.amountOfBullet > 0)
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
        GameObject projectile = Instantiate(this.bullet, spawnPos.position, ro);
        Projectile script = projectile.GetComponent<Projectile>();
        script.CreateBullet(damage, speed, timeLife);
    }

   
}
