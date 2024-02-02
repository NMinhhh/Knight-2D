using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();

    }
    private void Update()
    {
        
    }
    public void OpenDoor()
    {
        anim.SetBool("open", true);
    }



    public void CloseDoor()
    {
        anim.SetBool("open", false);
    }


}
