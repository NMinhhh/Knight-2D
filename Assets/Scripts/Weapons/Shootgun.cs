using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootgun : MonoBehaviour
{
    [Header("Shoting")]
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float timeLife;
    [SerializeField] private Transform[] attackPoint;
    [SerializeField] private GameObject bullet;

    [Header("Cooldown")]
    [SerializeField] private float coolDown;
    private float time;

    [Header("Reload")]
    private ReloadBullets reloadBullets;

    [Header("Sound")]
    [SerializeField] private AudioClip clip;

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
        if (InputManager.Instance.shoting && time >= coolDown && reloadBullets.amountOfBullet > 0)
        {
            time = 0;
            SoundFXManager.Instance.CreateAudioClip(clip, attackPoint[0], .5f);
            SpawnBullet(attackPoint[0], attackPoint[0].rotation);
            SpawnBullet(attackPoint[1], attackPoint[1].rotation);
            SpawnBullet(attackPoint[2], attackPoint[2].rotation);
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
