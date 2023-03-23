using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cucumber : Enemy,IDamageable
{
  

    public void SetOff()
    {
        targetPoint.GetComponent<Bomb>()?.TurnOff();
    }

    public void GetHit(float damage)
    {
        health -= damage;
        normalHealthBar.UpdateNormalHealthBar(health);
        if(health<=0)
        {
            health = 0;
            isDead = true;
            Destroy(normalHealthBar.transform.gameObject);//怪物死亡后血条消失
        }
        anim.SetTrigger("Hit");
    }
}
  