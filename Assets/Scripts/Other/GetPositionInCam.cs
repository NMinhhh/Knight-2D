using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPositionInCam : MonoBehaviour
{
    public static GetPositionInCam Instance { get; private set; }

    [SerializeField] private BoxCollider2D boxCollider;
    private Vector2 sizeBox;
    [SerializeField] private LayerMask whatIsEnemy;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        sizeBox = new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y);
    }

    public Vector2 GetPositionInArea()
    {
        Vector2 minBound = new Vector2(boxCollider.bounds.min.x, boxCollider.bounds.min.y);
        Vector2 maxBound = new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.max.y);

        float ranX = Random.Range(minBound.x, maxBound.x);
        float ranY = Random.Range(minBound.y, maxBound.y);

        return new Vector2(ranX, ranY);
    }

    public Collider2D[] GetEnemysPosition()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, sizeBox, 0, whatIsEnemy);
        return hits;
    }

}
