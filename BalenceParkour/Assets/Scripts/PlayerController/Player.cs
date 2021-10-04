using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator Anim;
    /// <summary>
    /// 射线起点
    /// </summary>
    public Transform[] rayStartPos;
    /// <summary>
    /// 移动速度
    /// </summary>
    public float walkSpeed;
    public float _walkSpeed => walkSpeed + MapManager.instance.moveSpeed;
    /// <summary>
    /// 跳跃速度
    /// </summary>
    public float jumpHeight;
    /// <summary>
    ///地面监测临界距离
    /// </summary>
    public float GroundHeight;
    /// <summary>
    /// 离地缓冲时间
    /// </summary>
    public float leaveGroundTime;
    public float jumpTime;//跳跃次数
    private Rigidbody2D rb;
    private float timeVal;
    /// <summary>
    /// 角色是否在地上
    /// </summary>
    private bool isGround;
    // {
    //     set
    //     {
    //         if (value)
    //             timeVal = Time.time;
    //     }
    //     get
    //     {
    //         if (Time.time - timeVal < leaveGroundTime)
    //             return true;
    //         else
    //             return false;
    //     }
    // }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        CheckInput();
    }
    private void Update()
    {
        CheckGround();
        Anim.SetBool("isGround", isGround);
    }
    public void CheckGround()
    {
        foreach (var item in rayStartPos)
        {
            Debug.DrawRay(item.position, Vector2.down * GroundHeight);
            if (Physics2D.Raycast(item.position, Vector2.down, GroundHeight, 1 << LayerMask.NameToLayer("Ground")))
            {
                if (!isGround)
                {
                    AudioManager.instance.ClipPlay(6);
                    CameraManager.instance.shakeTime = 0.05f;
                }
                isGround = true;
                return;
            }
        }
        isGround = false;
    }
    public void CheckInput()
    {
        // Debug.Log(InputManager.instance.GetTrigger("Jump"));

        if (InputManager.instance.GetTrigger("Right"))
        {
            rb.AddForce(Vector2.right * _walkSpeed);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        }
        if (InputManager.instance.GetTrigger("Left"))
        {
            rb.AddForce(Vector2.left * _walkSpeed);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
        }
        if (isGround && InputManager.instance.GetTrigger("Jump"))
        {
            jumpTime--;
            rb.AddForce(Vector2.up * jumpHeight * 1.5f, ForceMode2D.Impulse);
            CameraManager.instance.shakeTime = 0.1f;
            AudioManager.instance.ClipPlay(4);
            Anim.SetTrigger("Jump");
        }
        else if (!isGround && jumpTime > 0 && InputManager.instance.GetTrigger("Jump"))
        {
            jumpTime--;
            if (rb.velocity.y < 0)
                rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpHeight * 1.5f, ForceMode2D.Impulse);
            CameraManager.instance.shakeTime = 0.1f;
            AudioManager.instance.ClipPlay(5);
            Anim.SetTrigger("SegmentJump");
        }
        if (isGround && !Input.GetKey(KeyCode.Space))
            jumpTime = 2;
        if (Input.GetKey(KeyCode.Space))
            rb.gravityScale = 1.5f;
        else
            rb.gravityScale = 4;
        Anim.SetBool("KeyJump", Input.GetKey(KeyCode.Space));
    }
}
