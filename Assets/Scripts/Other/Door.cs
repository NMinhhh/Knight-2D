using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform checkPlayer;
    [SerializeField] private Vector2 size;
    [SerializeField] private LayerMask whatIsPlayer;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }
    private void Update()
    {
        if (CheckPlayer())
        {
            Close();
        }
    }
    void OpenDoor()
    {
        anim.SetBool("open", true);
    }

    bool CheckPlayer()
    {
        return Physics2D.OverlapBox(checkPlayer.position, size, 0, whatIsPlayer);
    }

    void Close()
    {
        anim.SetBool("open", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(checkPlayer.position, size);
    }
}
