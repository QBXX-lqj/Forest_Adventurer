using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public float percent;
    public Transform followingTarget;

    public Vector2 minPosition;
    public Vector2 maxPosition;

    void Start()
    {
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
         if (followingTarget != null)
        {
            Vector2 targetPosition = followingTarget.position;
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector2.Lerp(transform.position, targetPosition, percent);
        }       
    }
    void Update()
    {
        
    }
}
