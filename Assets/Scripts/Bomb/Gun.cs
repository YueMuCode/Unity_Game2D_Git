using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject shoot;
    void Fire()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            GameObject zidan = Instantiate(shoot, transform.position, shoot.transform.rotation);
            zidan.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.parent.lossyScale.x*10,0) , ForceMode2D.Impulse);//石头一个力，方向是scale乘以10，scale控制扔的方向。
        }
        
    }
 
    private void Update()
    {
        Fire();
    }
}
