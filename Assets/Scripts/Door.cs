using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
    BoxCollider2D coll;
    private void Start()
    {
        anim=GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        GameManager.instance.IsExitDoor(this);
        coll.enabled = false;
    }

    public void OpenDoor()
    {
        anim.Play("DoorOpen");
        coll.enabled = true;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //GameManager 去到下一个房间
            GameManager.instance.NextLevel();
        }
    }

}
