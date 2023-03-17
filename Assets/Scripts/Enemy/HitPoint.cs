using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    public bool bombAvilable;//特定的变量，规定给可以踢开炸弹的敌人
    int dir;//踢开的方向
    public float kickForce;//踢开的力



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.position.x > other.transform.position.x)
        {
            dir = -1;
        }
        else
        {
            dir = 1;
        }



        if(other.CompareTag("Player"))
        {
            Debug.Log("玩家受到伤害");
            other.GetComponent<IDamageable>().GetHit(1);
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 0.5f) * kickForce, ForceMode2D.Impulse);//踢开的力和方向
        }
        if(other.CompareTag("Bomb")&&bombAvilable)//如果检测到的是炸弹，并且这个敌人能够踢开的话。
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 0.5f) * kickForce, ForceMode2D.Impulse);//踢开的力和方向
        }
    }
}
