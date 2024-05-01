using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float startTime;
    public float time;

    private PolygonCollider2D attackHitBox;
    private Animator anim;

    void Start()
    {
        attackHitBox = GetComponent<PolygonCollider2D>();   
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AttackUpdate();
    }

    private void AttackUpdate()
    {
        if (Input.GetButtonDown("Attack"))
        {
            anim.SetTrigger("Attack");
            StartCoroutine(StartAttack());
        }
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);
        attackHitBox.enabled = true;
        StartCoroutine(EndAttack());
    }

    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(time);
        attackHitBox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
