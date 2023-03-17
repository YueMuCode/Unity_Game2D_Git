using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldPiate : Enemy,IDamageable
{
    public void GetHit(float damage)
    {
        health -= damage;
        normalHealthBar.UpdateNormalHealthBar(health);
        if (isBoss)
        {
            UIManager.instance.UpdateBossHealth(health);//实时更新boss血条
        }
        if (health < 1)
        {
            health = 0;
            isDead = true;
        }
        anim.SetTrigger("Hit");
       
    }
}
