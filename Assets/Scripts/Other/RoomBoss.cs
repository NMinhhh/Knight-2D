using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBoss : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private Vector2 limitPosX;
    [SerializeField] private Vector2 limitPosY;
    private Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        if (cam.position.x <= limitPosX.x)
        {
            pos.x = limitPosX.x;
        }
        else if(cam.position.x >= limitPosX.y)
        {
            pos.x = limitPosX.y;
        }
        else
        {
            pos.x = cam.position.x;
        }


        if(cam.position.y <= limitPosY.x)
        {
            pos.y = limitPosY.x;
        }
        else if (cam.position.y >= limitPosY.y)
        {
            pos.y = limitPosY.y;
        }
        else
        {
            pos.y = cam.position.y;
        }
        transform.position = pos;
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}
