using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBat : Enemy
{
    // Start is called before the first frame update
    public float speed;
    public float startWaitTime;
    private float waitTime;
    public Transform movePos;
    public Transform leftDown;
    public Transform rightUp;


    new public void Start()
    {
        base.Start();
        waitTime = startWaitTime;
        transform.position = GetRandomPosition();
    }

    // Update is called once per frame
    new public void Update()
    {
        base.Update();
        MoveAI();
    }

    private void MoveAI()
    {
        transform.position = Vector2.MoveTowards(
            transform.position, movePos.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePos.position) < 0.1f)
        {
            if (waitTime <= 0)
            {
                waitTime = startWaitTime;
                movePos.position = GetRandomPosition();
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private Vector2 GetRandomPosition()
    {
        Vector2 rndPos = new Vector2(
            Random.Range(leftDown.position.x, rightUp.position.x),
            Random.Range(leftDown.position.y, rightUp.position.y));
        return rndPos;
    }
}
