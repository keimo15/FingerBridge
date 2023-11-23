using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtle : MonoBehaviour
{
    Rigidbody2D rbody;                                              // 当たり判定
    public float axisH = 0.0f;                                             // 左右入力
    public float buttleSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()  // 物理系
    {  
        // 速度の更新
        if (axisH != 0)
        {
            // 地面の上 or 速度が 0 ではない
            rbody.velocity = new Vector2(axisH * buttleSpeed, rbody.velocity.y);
        }
    }
}
