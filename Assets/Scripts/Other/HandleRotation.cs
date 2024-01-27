using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandleRotation : MonoBehaviour
{
    private Vector3 direction;
    private float angle;
    private Player player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        HandleGunRotation();
    }

    void HandleGunRotation()
    {
        direction = (InputManager.Instance.mousePos - (Vector2)transform.position).normalized;
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
