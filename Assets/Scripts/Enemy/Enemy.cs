using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{

    public GameObject Sign;//获取头顶的标识
    [Header("Base State")]
    public float health;
    public bool isDead;
    public bool hasBomb;//是否已经有炸弹（作用于bigguy）
    public bool isBoss;//判断这个怪是否是boss

    EnemyBaseState currentState;
    public Animator anim;
    public int animState;//表示当前是哪一个状态，用于切换敌人的动画状态机
    [Header("Movement")]
    public float speed;//敌人移动的速度
    public Transform pointA, pointB;//敌人的基础巡逻范围
    public Transform targetPoint;//敌人的目标坐标
    public List<Transform> attackList = new List<Transform>();//可能检测范围内有敌人，炸弹，或者多个炸弹
    [Header("Attack Setting")]
    public float attackRate;//攻击间隔
    public float attackRange, skillRange;//攻击的范围
    private float nextAttack = 0;

    public PatrolState patrolState = new PatrolState();//获取Patrolstate类的对象
    public AttackState attackState = new AttackState();//来获取攻击状态的对象
    
    public virtual void Init()//这个方法的作用是获取动画机等一系列组件,单独一个函数的作用是，方便后面继承的子类，需要额外获取另外的组件
    {
        anim = GetComponent<Animator>();
        Sign = transform.GetChild(0).gameObject;
       
    }
    public void Awake()
    {
        Init();
    }

    public void Start()
    {
        TransitionToState(patrolState);
        if(isBoss)//如果是boss就把血量传给UI管理着
        {
            UIManager.instance.SetBossHealth(health);
          
        }
        GameManager.instance.AddEnemy(this);
    }
    public virtual void Update()
    {
        anim.SetBool("Dead", isDead);
        if (isDead)
        {
            GameManager.instance.RemoveEnemy(this);
            return;
        }
        currentState.OnUpdate(this);
        anim.SetInteger("State", animState);
       
    }
    public void TransitionToState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    public void MoveToTarget()//移动到特定的目标
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        FilpDirection();
    }
    public virtual void AttackAction()//攻击行为让子类继承实现
    {
      
        if(Vector2.Distance(transform.position,targetPoint.position)<attackRange)//敌人进入了攻击范围
        {
            if(Time.time>nextAttack)
            {
                //播放攻击动画
                Debug.Log("普通攻击");
                anim.SetTrigger("Attack");               
                nextAttack = Time.time + attackRange;
            }
        }
    }
    public virtual void SkillAction()//特殊攻击的行为让子类继承实现
    {
        if (Vector2.Distance(transform.position, targetPoint.position) < skillRange)//敌人进入了攻击范围
        {
            if (Time.time > nextAttack)
            {
                //播放攻击动画
                anim.SetTrigger("Skill");
                Debug.Log("特殊攻击");
                nextAttack = Time.time + attackRange;
            }
        }
    }
    public void FilpDirection()//人物的转身
    {
        if(transform.position.x<targetPoint.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    public void SwitchPoint()
    {
        if(Mathf.Abs(pointA.position.x-transform.position.x)>Mathf.Abs(pointB.position.x-transform.position.x))
        {
            targetPoint = pointA;
        }else
        {
            targetPoint = pointB;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(!attackList.Contains(collision.transform)&&!hasBomb&&!isDead&&!GameManager.instance.gameOver)
        {
            attackList.Add(collision.transform);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        attackList.Remove(collision.transform);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isDead && !GameManager.instance.gameOver)
        {
            StartCoroutine(OnSign());
        }
    }
    IEnumerator OnSign()
    {
        Sign.SetActive(true);
        yield return new WaitForSeconds(Sign.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
        Sign.SetActive(false);
    }


}
