using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandleRotation : MonoBehaviour
{
    private Vector3 direction;
    public float angle {  get; private set; }
    private Player player;
    public bool auto {  get; private set; }
    [Header("Auto")]
    private GameObject[] allObj;
    public GameObject nearestObj {  get; private set; }
    private float distance;
    private float nearestDistance = 15;


    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (auto) 
        {
            HandleGunRotationEnemy();
        }
        else
        {
            HandleGunRotation(InputManager.Instance.mousePos);

        }
    }

    void HandleGunRotationEnemy()
    {
        allObj = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < allObj.Length; i++)
        {
            distance = Vector3.Distance(transform.position, allObj[i].transform.position);
            if(distance < nearestDistance)
            {
                nearestObj = allObj[i];
                nearestDistance = distance;
            }
        }
        if(nearestObj != null)
        {
            HandleGunRotation(nearestObj.transform.position);
        }
        else
        {
            nearestDistance = 1000;
        }
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

    public void TurnAuto()
    {
        auto = !auto;
    }
}
