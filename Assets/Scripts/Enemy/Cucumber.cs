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
        if(health<1)
        {
            health = 0;
            isDead = true;
        }
        anim.SetTrigger("Hit");
    }
}
  