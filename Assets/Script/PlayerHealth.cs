using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public float timePerBlink;
    public int numOfBlinks;
    public float deathTime;

    public Renderer playerRenderer;
    public Animator animator;

    /*private const PlayerHealthBar HEALTH_BAT = PlayerHealthBar.SELT;*/

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        animator = GetComponent<Animator>();

        PlayerHealthBar.maxHealthValue = health; // 初始化血条最大血量为玩家当前血量
        PlayerHealthBar.currentValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damaged(int damage)
    {
        health -= damage;
        PlayerHealthBar.currentValue = health < 0 ? 0 : health; 
        if (health <= 0)
        {
            if (animator != null)
            {
                animator.SetTrigger("Die");
            }
            Invoke("KillPlayer", deathTime);
        }
        BlinkPlayer(numOfBlinks, timePerBlink);
    }

    private void KillPlayer()
    {
        Destroy(gameObject);
    }

    private void BlinkPlayer(int blinkNum, float time)
    {
        Debug.Log("Blink Start");
        StartCoroutine(BlinksOfTime(blinkNum, time));
    }

    private IEnumerator BlinksOfTime(int blinkNum, float time)
    {
        for (int i = 0; i < blinkNum * 2; ++i)
        {
            playerRenderer.enabled = !playerRenderer.enabled;
            yield return new WaitForSeconds(time);
        }
        playerRenderer.enabled = true;
    }
}
