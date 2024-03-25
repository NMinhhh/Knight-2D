using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBoss : MonoBehaviour
{
    [SerializeField] private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
