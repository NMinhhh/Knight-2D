using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Transform checkPlayer;
    [SerializeField] private Vector2 size;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Door door1;
    [SerializeField] private Door door2;
    [SerializeField] private List<GameObject> spawner;
    private bool isWin;
    void Start()
    {
        door1.OpenDoor();
        door2.CloseDoor();

    }

    // Update is called once per frame
    void Update()
    {
        if (CheckPlayer())
        {
            door1.CloseDoor();
            foreach (GameObject go in spawner)
            {
                go.SetActive(true);
            }
        }
        for (int i = 0; i < spawner.Count; i++)
        {
            if (spawner[i] == null)
            {
                spawner.RemoveAt(i);
            }
        }
        if (spawner.Count <= 0)
        {
            door1.OpenDoor();
            door2.OpenDoor();
        }

    }

    bool CheckPlayer()
    {
        return Physics2D.OverlapBox(checkPlayer.position, size, 0, whatIsPlayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(checkPlayer.position, size);
    }
}
