using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : Enemy, IDamageable
{
    public void GetHit(float damage)
    {
   
            health -= damage;
            normalHealthBar.UpdateNormalHealthBar(health);
            if (health <=0)
            {
                health = 0;
                isDead = true;
            Destroy(normalHealthBar.transform.gameObject);//怪物死亡后血条消失
        }
            anim.SetTrigger("Hit");
     }

    public void Swalow()//动画事件触发的事件，吞下炸弹
    {
        if(targetPoint!=null)
        {
            targetPoint.GetComponent<Bomb>().TurnOff();
            targetPoint.gameObject.SetActive(false);
        }
    }
}
