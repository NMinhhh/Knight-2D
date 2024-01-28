using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float timeLife;
    [SerializeField] private float cooldownTimer;
    [SerializeField] private AudioClip clip;
    private float timer;
    private HandleRotation handleRotation;

    private void Start()
    {
        handleRotation = GetComponent<HandleRotation>();
    }

    void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        timer += Time.deltaTime;
        if(InputManager.Instance.shoting && timer >= cooldownTimer)
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
        }
    }

}
