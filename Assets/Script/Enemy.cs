using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float health;
    public int damage;
    public float flashTime;
    public Color damagedColor;
    public GameObject booldEffect;

    private SpriteRenderer spriteRenderer;
    private Color originColor;

    public PlayerHealth playerHealth;

    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        FlashColor(flashTime);
        Instantiate(booldEffect, transform.position, Quaternion.identity);
        GameController.camShake.Shake();
    }

    private void FlashColor(float time)
    {
        spriteRenderer.color = damagedColor;
        Invoke("ResetColor", time);
    }

    private void ResetColor()
    {
        spriteRenderer.color = originColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") 
            && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth != null)
            {
                playerHealth.Damaged(damage);
            }
        }
    }
}
