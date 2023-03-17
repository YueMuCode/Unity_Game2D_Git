using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        Debug.Log("发现敌人！！！");
        enemy.animState = 2;
        enemy.targetPoint = enemy.attackList[0];//默认跟随检测到的第一个
    }

    public override void OnUpdate(Enemy enemy)
    {
        if(enemy.hasBomb)//手上有炸弹下面的都不执行了
        {
            return;
        }
        if(enemy.attackList.Count==0)
        {
            enemy.TransitionToState(enemy.patrolState);
        }
        if(enemy.attackList.Count>1)
        {
            for(int i=0;i<enemy.attackList.Count;i++)
            {
                if(Mathf.Abs(enemy.transform.position.x-enemy.attackList[i].position.x)<
                   Mathf.Abs(enemy.transform.position.x-enemy.targetPoint.position.x))//如果出现了一个目标距离正在追赶的目标更近的话，改变追击的目标
                {
                    enemy.targetPoint = enemy.attackList[i];
                }

                
            }
        }
        if(enemy.attackList.Count==1)
        {
            enemy.targetPoint = enemy.attackList[0];//如果只有一个的情况
        }
        if(enemy.targetPoint.CompareTag("Player"))
        {
            enemy.AttackAction();
        }
        if (enemy.targetPoint.CompareTag("Bomb"))
        {
            enemy.SkillAction();
        }
        enemy.MoveToTarget();
    }
}
