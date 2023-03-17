using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator anim;//获取炸弹的动画组件
    public float startTime;//爆炸开始点燃
    public float waitTime;//点燃后的等待时间
    public float bombForce;//爆炸的力度

    [Header("Check")]
    public float radius;//爆炸的范围
    public LayerMask targetLayer;//获取炸弹爆炸影响的图层。





    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        startTime = Time.time;//炸弹的点燃时间是游戏的时钟
    }

    // Update is called once per frame
    void Update()
    {
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Bomb_Off"))//如果炸点不被吹灭就执行爆炸动画，如果执行吹灭就不执行
        {
            if (Time.time > startTime + waitTime)
            {
                anim.Play("Bomb_Explotion");
            }
        }
    }

    public void OnDrawGizmos()//画出范围作用Debug
    {
        Gizmos.DrawWireSphere(transform.position+new Vector3(0.03f,-0.15f,0.1f), radius);
    }
    public void ExploTion()//只能是在爆炸的一瞬间触发，那应该放在动画里面触发
    {
        Collider2D[] aroundObjects = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);//获取炸弹特定范围内规定好的图层内的所有物体的collier2d组件。
        foreach(var item in aroundObjects)//遍历每一个拿到的物体的
        {
            Vector3 pos = transform.position - item.transform.position; //获取炸弹和被炸飞的物体之间的方向
            item.GetComponent<Rigidbody2D>().AddForce((-pos+Vector3.up) * bombForce, ForceMode2D.Impulse);//给一个反方先的冲击力

            if(item.CompareTag("Bomb")&&item.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Bomb_Off"))//如果一个爆炸的炸弹获取的周围物体中有一个
                //被熄灭的了的炸弹的话，我们就获取这个炸弹的代码脚本，执行里面的TurnOn 函数
            {
                item.GetComponent<Bomb>().TurnOn();
            }
            if(item.CompareTag("Player"))
            {
                item.GetComponent<IDamageable>().GetHit(3);
            }


        }
    }

    public void DestroyThis()//爆炸结束后销毁
    {
        Destroy(gameObject);
    }
   public void TurnOff()//炸弹熄灭作用
    {
        anim.Play("Bomb_Off");
        gameObject.layer = LayerMask.NameToLayer("NPC");//防止吹灭后敌人仍然把炸弹作为攻击目标，所以把炸弹的图层换成NOC
    }
    public void TurnOn()//炸弹重新启动作用
    {
        startTime = Time.time;//为了防止炸弹直接启动，重新记录它的开启时间
        anim.Play("Bomb_On");
        gameObject.layer = LayerMask.NameToLayer("Bomb");
    }

}
