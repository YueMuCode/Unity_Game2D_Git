using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour,IDamageable
{
    private Animator anim;
    private FixedJoystick joystick;//手机移动

    private Rigidbody2D rb;//接收获取的刚体对象组件
    public float speed;//获取移动的速度
    public float jumpForce;//获取跳跃的力


    [Header("Player State")]
    public float health;
    public bool isDead;


    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;
    [Header("States Check")]
    public bool isGround;
    public bool IsJump;//用于判断触发跳跃动画
    public bool canJump;//是否按下了跳跃键
    [Header("GameObject FX")]
    public GameObject LandFX;//获取落地的特效组件
    public GameObject JumpFX;//获取跳跃的落地组件
    [Header("Attack Settings")]
    public GameObject bombPrefab;//获取炸弹的预制体
    public float nextAttack = 0;//攻击冷却时间冷却时间
    public float attackRate;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//获取角色上的刚体组件。
        anim = GetComponent<Animator>();
        GameManager.instance.IsPlayer(this);
        health = GameManager.instance.LoadHealth();
        UIManager.instance.UpdateHealth(health);
        joystick = FindObjectOfType<FixedJoystick>();
    }

    // Update is called once per frame
    void Update()//每帧执行一次
    {
        anim.SetBool("Dead", isDead);//在update中实时检测是否已经死亡
        if (isDead) return;
        CheckInput();
        PhysicsCheck();
       
    }
    void FixedUpdate()//固定每秒五十次
    {
        if(isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Movement();//调用移动
        Jump();//调用跳跃


    }
    void CheckInput()//检测是否按下了跳跃键
    {
        if(Input.GetButtonDown("Jump")&&isGround)
        {
            canJump = true;
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
    }
    void Movement()//控制移动的方法
    {
        //键盘操作
         float x = Input.GetAxisRaw("Horizontal");//按下ad或者左右键，x的值在-1到1 之间变化（有Raw不包括小数）
        if (x != 0)
        {
            transform.localScale = new Vector3(x, 1, 1);//将scale或者rotation的x值变化到达翻转的效果  
        }
        // Debug.Log(x);

        //操作杆
      // float x = joystick.Horizontal;
        rb.velocity = new Vector2(x * speed, rb.velocity.y);//利用刚体组件，控制人物的移动，x轴给一个速度和方向的乘积，y保持不变

       
        //if (x>0)
        //{
        //    transform.eulerAngles = new Vector3(0, 0, 0);
        //}
        //if(x<0)
        //{
        //    transform.eulerAngles = new Vector3(0, 180, 0);
        //}

    }

    public void Jump()
    {
        if(canJump)
        {
            IsJump = true;
            JumpFX.SetActive(true);//播放动画的时机
            JumpFX.transform.position = transform.position + new Vector3(0.1f, -0.5f, 0);//播放动画的位置
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            rb.gravityScale = 3.5f;
            canJump = false;//跳跃之后将判断条件改回false； 
        }
    }
    public void ButtonJump()
    {
        if(isGround)
        {
            canJump = true;
        }
    }
    public void Attack()
    {
        if (Time.time > nextAttack)
        {
            Instantiate(bombPrefab, transform.position, bombPrefab.transform.rotation);//生成炸弹的预制体
            nextAttack = Time.time + attackRate;//下一次的攻击时间
        }
    }


    void PhysicsCheck()//检测角色脚下那个对象的的附近的范围。
    {
        //isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        isGround = Physics2D.OverlapBox(groundCheck.position, new Vector2(checkRadius, checkRadius), 0,groundLayer);
     
        if(isGround)
        {
            rb.gravityScale = 1;
           // 写在这里会导致人物在地面的时候也一直播放动画
           // LandFX.SetActive(true);//播放动画的时机
            //LandFX.transform.position = transform.position + new Vector3(0.15f, -0.75f, 0);
            IsJump = false;
        }else
        {
            rb.gravityScale = 3.5f;
        }
    }

    #region LandFX
    public void LandFXStart()
    {
        LandFX.SetActive(true);//播放动画的时机
        LandFX.transform.position = transform.position + new Vector3(0.15f, -0.75f, 0);
    }

    #endregion


    public void OnDrawGizmos()
    {
       // Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        Gizmos.DrawWireCube(groundCheck.position, new Vector3(checkRadius, checkRadius, checkRadius));
    }

    public void GetHit(float damage)
    {
        if (!anim.GetCurrentAnimatorStateInfo(1).IsName("Player_Hit"))//如果正在播放受伤动画就不能受到伤害。
        {
            health -= damage;
            if (health < 1)
            {
                health = 0;
                isDead = true;
            }
            anim.SetTrigger("Hit");
        }
        UIManager.instance.UpdateHealth(health);
       
    }
}
