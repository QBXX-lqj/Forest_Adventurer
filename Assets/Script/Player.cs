using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public float doubleJumpSpeed;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private BoxCollider2D feet;
    private PlayerHealth playerHealth;

    private int extraJumpPower;
    private bool onGround;

    private enum ActionAnim{ Jump, DoubleJump, Move, Idle, Fall };
    private ActionAnim actionAnim;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        feet = GetComponent<BoxCollider2D>();
        playerHealth = GetComponent<PlayerHealth>();

        extraJumpPower = 1;
    }

    // Update is called once per frame
    void Update()
    {
        CheckOnGround();

        //Debug.Log(actionAnim);
        
        if (playerHealth.health > 0)
        {
            MoveUpdate();
            JumpUpdate();
            FallUpdate();
            ActionSwitch();
        }
        else
        {
            rigidBody.velocity = rigidBody.velocity * new Vector2(0, 1);
        }
    }

    private void CheckOnGround()
    {
        onGround = feet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private void Flip(float moveDir)
    {
        if (moveDir > 0.25f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveDir < -0.25f)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void FallUpdate()
    {
        if (rigidBody.velocity.y < 0)
        {
            //actionAnim = ActionAnim.Fall;
            animator.SetBool("Fall", true);
            animator.SetBool("Idle", false);
            if (animator.GetBool("Jump"))
            {
                animator.SetBool("Jump", false);
            }
            if (animator.GetBool("DoubleJump"))
            {
                animator.SetBool("DoubleJump", false);
            }
        }
        else
        {
            if (onGround && animator.GetBool("Fall"))
            {
                animator.SetBool("Idle", true);
            }
            animator.SetBool("Fall", false);
        }
    }

    private void ActionSwitch()
    {
       /* animator.SetBool("Idle", false);
        animator.SetBool("Jump", false);
        animator.SetBool("DoubleJump", false);
        animator.SetBool("Fall", false);
        animator.SetBool("Move", false);
        if (actionAnim == ActionAnim.Jump)
        {
            animator.SetBool("Jump", true);
        }
        else if (actionAnim == ActionAnim.DoubleJump)
        {
            animator.SetBool("DoubleJump", true);
        }
        else if (actionAnim == ActionAnim.Move)
        {
            animator.SetBool("Move", true);
        }
        else if (actionAnim == ActionAnim.Fall)
        {
            animator.SetBool("Fall", true);
        }*/
    }

    private void Jump(string animBoolName)
    {
        // 实现跳跃效果
        Vector2 jumpVec = new Vector2(rigidBody.velocity.x, jumpSpeed);
        rigidBody.velocity = jumpVec;

        animator.SetBool(animBoolName, true);
        animator.SetBool("Idle", false);
    }

    private void JumpUpdate()
    {
        bool isJump = Input.GetButtonDown("Jump");

        if (isJump)
        {
            // 跳跃条件
            if (onGround)
            {
                Jump("Jump");
                actionAnim = ActionAnim.Jump;
                extraJumpPower = 1;
            }
            else if (extraJumpPower != 0)
            {
                Jump("DoubleJump");
                actionAnim = ActionAnim.DoubleJump;
                extraJumpPower--;
            }
        }
    }

    private void MoveUpdate()
    {
        float moveDir = Input.GetAxisRaw("Horizontal");

        // 设置速度，实现移动效果
        Vector2 moveVec = new Vector2(moveDir * moveSpeed, rigidBody.velocity.y);
        rigidBody.velocity = moveVec;
        // 实现左右移动的翻转
        Flip(moveDir);
        // 移动动画
        if (Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon)
        {
            actionAnim = ActionAnim.Move;
            animator.SetBool("Move", true);
        }
        else animator.SetBool("Move", false);
    }
}
