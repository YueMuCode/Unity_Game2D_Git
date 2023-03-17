using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations; 
public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;//获取动画播放器组件
    private Rigidbody2D rb;//获取人物身上的刚体
    private PlayerController PlayerController;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();//获取动画控制器
        rb = GetComponent<Rigidbody2D>();//获取刚体
        PlayerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));//向左走速度会小于零。
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("Jump", PlayerController.IsJump);
        anim.SetBool("Ground", PlayerController.isGround);
    }
}
