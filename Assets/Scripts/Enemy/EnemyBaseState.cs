
public abstract class EnemyBaseState //抽象类，让子类实现
{
    public abstract void EnterState(Enemy enemy);
    public abstract void OnUpdate(Enemy enemy);
}
