using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : Enemy, IDamageable
{
    public SpriteRenderer sprite;
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
    public override void Update()
    {
        base.Update();
        if(animState==0)
        {
            sprite.flipX = false;
        }
    }
    public override void Init()
    {
        base.Init();
        sprite = GetComponent<SpriteRenderer>();
    }

    public override void SkillAction()
    {
        base.SkillAction();
        if(anim.GetCurrentAnimatorStateInfo(1).IsName("Skill"))//如果正在播放动画skill，
        {
            sprite.flipX = true;
            if (transform.position.x > targetPoint.position.x)//人在炸弹的右边
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.right, speed * 2 * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.right, speed * 2 * Time.deltaTime);
            }
        }
        else
        {
            sprite.flipX = false;
        }
    }
}
