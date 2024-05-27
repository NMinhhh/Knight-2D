using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRotation : MonoBehaviour
{
    private Vector3 direction;
    public float angle {  get; private set; }
    public Player player {  get; private set; }
    public bool isShooting {  get; private set; }

    public GameObject nearestObj {  get; private set; }
    private float distance;
    private float nearestDistance = 100;


    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (AutoShoot.isLockHandle)
        {
            if(AutoShoot.nearestObj != null)
            {
                HandleGunRotation(AutoShoot.nearestObj.transform.position);
                AutoShoot.SetAutoShoot(true);
            }
        }
        else if (nearestObj != null)
        {
            HandleGunRotationEnemy();
            AutoShoot.SetNearestObj(nearestObj);
            AutoShoot.SetAutoShoot(true);
        }
        else if (GetPositionInCam.Instance.GetEnemysPosition().Length > 0) 
        {
            HandleGunRotationEnemy();
        }
        else if(nearestObj == null)
        {
            AutoShoot.SetAutoShoot(false);
            if (InputManager.Instance.xInput == 0 || InputManager.Instance.yInput == 0 || AutoShoot.isLockHandle)
                return;
            HandleGunRotationToJoyStick();

        }
    }

    void HandleGunRotationEnemy()
    {
        Collider2D[] enemys = GetPositionInCam.Instance.GetEnemysPosition();

        for(int i = 0; i < enemys.Length; i++)
        {
            distance = Vector3.Distance(transform.position, enemys[i].transform.position);
            if(distance < nearestDistance)
            {
                nearestObj = enemys[i].gameObject;
                nearestDistance = distance;     
            }
        }
        if(nearestObj != null)
        {
            HandleGunRotation(nearestObj.transform.position);
        }
        else
        {
            nearestDistance = 100;
        }
    }

    void HandleGunRotationToJoyStick()
    {
        direction = (new Vector2(InputManager.Instance.xInput, InputManager.Instance.yInput)).normalized;
        transform.right = direction;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector3 localScale = Vector3.one;

        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = 1f;
        }

        if (player.isFacingRight)
        {
            localScale.x = 1f;
        }
        else
        {
            localScale.x = -1f;
        }
        transform.localScale = localScale;
    }

    void HandleGunRotation(Vector2 taget)
    {
        direction = (taget - (Vector2)transform.position).normalized;
        transform.right = direction;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector3 localScale = Vector3.one;

        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = 1f;
        }

        if (player.isFacingRight)
        {
            localScale.x = 1f;
        }
        else
        {
            localScale.x = -1f;
        }
        transform.localScale = localScale;
    }
}
