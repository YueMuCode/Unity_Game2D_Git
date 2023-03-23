using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIgGuy : Enemy,IDamageable
{
    public Transform pickupPoint;//拿起炸弹的坐标
    public float power;
    public void GetHit(float damage)
    {
        anim.SetTrigger("Hit");
        health -= damage;
        normalHealthBar.UpdateNormalHealthBar(health);
        if (health <=0)
        {
            health = 0;
            isDead = true;
            Destroy(normalHealthBar.transform.gameObject, 1f);//怪物死亡后血条消失
            UIManager.instance.AddScore(10);
        }
        
    }
    public void PickBomb()//动画事件调用
    {
        if(targetPoint.CompareTag("Bomb")&&!hasBomb)
        {
            targetPoint.gameObject.transform.position = pickupPoint.position;//改变炸弹的坐标
            targetPoint.SetParent(pickupPoint);//将炸弹变为子集，实现拿着移动
            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;//改变刚体组件，防止炸弹掉落
            hasBomb = true;
        }
    }
    public void ThrowAway()
    {
        if(hasBomb)
        {
            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;//改回有重力的形式
            targetPoint.SetParent(transform.parent.parent);//调换炸弹的层级，防止扔出去的时候跟着人物移动

            if(FindObjectOfType<PlayerController>().gameObject.transform.position.x-transform.position.x<0)
            {
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0.5f) * power, ForceMode2D.Impulse);
            }
            else
            {
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0.5f) * power, ForceMode2D.Impulse);
            }
        }
        hasBomb = false;
    }
}
