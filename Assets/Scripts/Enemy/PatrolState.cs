using UnityEngine;
public class PatrolState : EnemyBaseState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.animState = 0;//当前的状态是0，即idle状态
        enemy.SwitchPoint();
    }

    public override void OnUpdate(Enemy enemy)
    {
        if (!enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))//获取当前动画机播放的状态的名字，idle；，如果不是这个状态，说明播放完了（设置成不是循环）
        {
            enemy.animState = 1;
            enemy.MoveToTarget();
           // Debug.Log("执行了移动");
        }
        if (Mathf.Abs(enemy.transform.position.x-enemy.targetPoint.position.x)<0.01f)
        {
           // enemy.SwitchPoint();如果到达目的地，会切换状态，不需要在这里切换目标点
            enemy.TransitionToState(enemy.patrolState);
        }

        if(enemy.attackList.Count>0)//如果检测的list里面有物体就切换到攻击状态
        {
            enemy.TransitionToState(enemy.attackState);
        }
       
    }
}