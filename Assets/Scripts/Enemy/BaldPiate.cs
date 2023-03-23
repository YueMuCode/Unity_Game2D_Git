using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldPiate : Enemy,IDamageable
{
    public void GetHit(float damage)
    {
        anim.SetTrigger("Hit");
        health -= damage;
        normalHealthBar.UpdateNormalHealthBar(health);
        if (isBoss)
        {
            UIManager.instance.UpdateBossHealth(health);//实时更新boss血条
        }
        if (health <=0)
        {
            health = 0;
            isDead = true;
            Destroy(normalHealthBar.transform.gameObject);//怪物死亡后血条消失
        }
       
       
    }
}
