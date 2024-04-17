using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRotation : MonoBehaviour
{
    private Vector3 direction;
    public float angle {  get; private set; }
    public Player player {  get; private set; }
    public bool auto {  get; private set; }

    public GameObject nearestObj {  get; private set; }
    private float distance;
    private float nearestDistance = 15;


    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (EnemysPosition.Instance.GetEnemysPosition().Length > 0) 
        {
            HandleGunRotationEnemy();
        }
        else
        {
            if (InputManager.Instance.xInput == 0 || InputManager.Instance.yInput == 0)
                return;
            HandleGunRotationToJoyStick();

        }
    }

    void HandleGunRotationEnemy()
    {
        Collider2D[] enemys = EnemysPosition.Instance.GetEnemysPosition();

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
            nearestDistance = 15;
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

    public void TurnOnAuto()
    {
        auto = true;
    }

    public void TurnOffAuto()
    {
        auto = false;

    }
}
