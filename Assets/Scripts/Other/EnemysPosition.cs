using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysPosition : MonoBehaviour
{
    public static EnemysPosition Instance;

    [SerializeField] private Transform checkPoint;
    [SerializeField] private Vector2 sizeBox;
    [SerializeField] private LayerMask whatIsEnemy;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Collider2D[] GetEnemysPositio()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(checkPoint.position, sizeBox, 0, whatIsEnemy);
        return hits;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(checkPoint.position, sizeBox);
    }
}
