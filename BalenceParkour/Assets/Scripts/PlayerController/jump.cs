using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public float jump_speed = 10f;  //跳跃时间，从松开空格键开始计算
    public float jumpHoldSpeed = 4f;
    public bool jumpPress = false;
    public bool jumpHold = false;//长按跳跃检测

    public int allowJumpTimes;// 允许的跳跃次数
    private int airJumpCount;// 空中跳跃计数
    float jumpMaxTime = 2f;

    float jumpTimer;//长按跳跃计时器
    public bool isGrounded;// 判断是否触地
    public LayerMask whatIsGround;// 地面的layermask

    private Rigidbody2D rb;

    private Animator anim;
    public float boxHeight;//落地检测盒子
    private Vector2 playerSize;
    private Vector2 boxSize;


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        playerSize = GetComponent<SpriteRenderer>().bounds.size;
        boxSize = new Vector2(playerSize.x * 0.8f, boxHeight);

        airJumpCount = 0;
        jumpTimer = jumpMaxTime;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("jump"))
        {
            jumpPress = true;
        }

        if (Input.GetButton("jump") && jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
            jumpHold = true;
        }
        else
        {
            jumpTimer = jumpMaxTime;
        }

    }

    private void FixedUpdate()
    {
        Jump();
        SwitchAnimState();
    }

    /// <summary>
    /// 跳跃（二段跳与长按跳跃,落地检测）
    /// </summary>
    private void Jump()
    {
        if (jumpPress)
        {
            if (airJumpCount <= 0)
            {
                return;
            }
            else if (isGrounded && airJumpCount == 0)
            {
                rb.AddForce(Vector2.up * jump_speed, ForceMode2D.Impulse);
                if (jumpHold)
                {
                    rb.AddForce(Vector2.up * jumpHoldSpeed, ForceMode2D.Impulse);
                }
            }
            else if (allowJumpTimes - 1 > airJumpCount)  //airjumpcount 從0開始計數
            {
                rb.AddForce(Vector2.up * jump_speed, ForceMode2D.Impulse);
                airJumpCount++;
            }

            jumpPress = false;
            isGrounded = false;
        }
        else   //落地检测
        {
            Vector2 boxCenter = (Vector2)transform.position + (Vector2.down * playerSize.y * 0.5f);

            if (Physics2D.OverlapBox(boxCenter, boxSize, 0, whatIsGround) != null)
            {
                isGrounded = true;
                airJumpCount = 0;
            }
            else
            {
                isGrounded = false;
            }
        }

    }


    /// <summary>
    /// 碰撞盒子检测函数
    /// </summary>
    private void OnDrawGizmos()
    {
        if (isGrounded)
        {
            Gizmos.color = Color.red;

        }
        else
        {
            Gizmos.color = Color.green;
        }

        Vector2 boxCenter = (Vector2)transform.position + (Vector2.down * playerSize.y * 0.5f);
        Gizmos.DrawCube(boxCenter, boxSize);
    }

    /// <summary>
    /// 动画效果
    /// </summary>
    private void SwitchAnimState()
    {

    }
}
