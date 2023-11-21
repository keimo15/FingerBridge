using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 色を変える処理と、周囲のブロックの判定

public enum BW
{
    Black,
    White,
}

public class BWChanger : MonoBehaviour
{
    string blackGroundTagName = "BlackGround";
    string whiteGroundTagName = "WhiteGround";

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ToWhiteChange(GameObject triangle)
    {
        triangle.GetComponent<SpriteRenderer>().color = Color.white;
        triangle.layer = LayerMask.NameToLayer(whiteGroundTagName);
        Debug.Log("白に");
    }

    public void ToBlackChange(GameObject triangle)
    {
        triangle.GetComponent<SpriteRenderer>().color = Color.black;
        triangle.layer = LayerMask.NameToLayer(blackGroundTagName);
        Debug.Log("黒に");
    }
}
