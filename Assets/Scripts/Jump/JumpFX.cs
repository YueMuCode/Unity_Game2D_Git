using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFX : MonoBehaviour
{
    public void Finish()//利用动画的关键帧调用，到达关键帧的时候会直接关闭这个对象即FX 
    {
        gameObject.SetActive(false);
    }
}
