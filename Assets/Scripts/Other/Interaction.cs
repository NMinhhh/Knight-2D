using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    [SerializeField] private Transform checkPoint;
    [SerializeField] private float radius;

    [SerializeField] private LayerMask whatIsStore;

    [SerializeField] private GameObject storeUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.Instance.keyEnter && CheckStore())
        {
            storeUI.SetActive(true);
        }
    }

    bool CheckStore()
    {
        return Physics2D.OverlapCircle(checkPoint.position, radius, whatIsStore);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkPoint.position, radius);
    }
}
