using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ブロックの情報  

public class DrowBlock : MonoBehaviour
{
    public Vector2 blockPosition;                               // ブロックの座標   
    [SerializeField] GameObject[] triangles;     // 0:上, 1:左, 2:下, 3:右

    //子オブジェクトの全て（子の子の子のオブジェクトとかも）を取得する
    void Start()
    {
        
    }

    // 子オブジェクトの色を変える
    public void ToWhiteBlock()
    {
        BWChanger bWChanger = GetComponent<BWChanger>();
        foreach(GameObject triangle in triangles)
        {
            bWChanger.ToWhiteChange(triangle);
        }
    }
    public void ToBlackBlock()
    {
        BWChanger bWChanger = GetComponent<BWChanger>();
        foreach(GameObject triangle in triangles)
        {
            bWChanger.ToBlackChange(triangle);
        }
    }
}
