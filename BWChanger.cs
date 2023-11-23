using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 色を変える処理と、周囲のブロックの判定

public class BWChanger : MonoBehaviour
{
    bool isBlack;                                       // ブロックが黒であるか
    public int positionX;                               // ブロックの x 座標
    public int positionY;                               // ブロックの y 座標
    [SerializeField] GameObject[] triangles;            // 子要素の三角形, 0:上, 1:左, 2:下, 3:右
    string blackGroundLayerName = "BlackGround";        // 黒の地面判定
    string whiteGroundLayerName = "WhiteGround";        // 白の地面判定

    void Start()
    {
        isBlack = true;
    }

    void Update()
    {
        
    }

    // 子オブジェクトの色を変える
    public void ToWhiteBlock()
    {
        BlockManager.areBlack[positionX, positionY] = false;

        // 外周のマスは常に四角単位で色が変わる
        if (positionX == 0 || positionX == BlockManager.rows-1 || positionY == 0 || positionY == BlockManager.columns-1)
            foreach(GameObject triangle in triangles) ToWhiteTriangle(triangle);
    
        // 左と上が白 && 右と下が黒 -> (三角形) 左と上を白に
        else if (!BlockManager.areBlack[positionX-1, positionY] && !BlockManager.areBlack[positionX, positionY+1])
        {
            ToWhiteTriangle(triangles[0]); ToWhiteTriangle(triangles[1]);
        }

        // 左と下が白 && 右と上が黒 -> (三角形) 左と下を白に
        else if (!BlockManager.areBlack[positionX-1, positionY] && !BlockManager.areBlack[positionX, positionY-1])
        {
            ToWhiteTriangle(triangles[1]); ToWhiteTriangle(triangles[2]);
        }

        // 右と下が白 && 左と上が黒 -> (三角形) 右と下を白に
        else if (!BlockManager.areBlack[positionX+1, positionY] && !BlockManager.areBlack[positionX, positionY-1])
        {
            ToWhiteTriangle(triangles[2]); ToWhiteTriangle(triangles[3]);
        }

        // 右と上が白 && 左と下が黒 -> (三角形) 右と上を白に
        else if (!BlockManager.areBlack[positionX+1, positionY] && !BlockManager.areBlack[positionX, positionY+1])
        {
            ToWhiteTriangle(triangles[0]); ToWhiteTriangle(triangles[3]);
        }

        else foreach(GameObject triangle in triangles) ToWhiteTriangle(triangle);
    }
    public void ToBlackBlock()
    {
        BlockManager.areBlack[positionX, positionY] = true;
        foreach(GameObject triangle in triangles)
        {
            ToBlackTriangle(triangle);
        }
    }

    public void ToWhiteTriangle(GameObject triangle)
    {
        triangle.GetComponent<SpriteRenderer>().color = Color.white;
        triangle.layer = LayerMask.NameToLayer(whiteGroundLayerName);
    }

    public void ToBlackTriangle(GameObject triangle)
    {
        triangle.GetComponent<SpriteRenderer>().color = Color.black;
        triangle.layer = LayerMask.NameToLayer(blackGroundLayerName);
    }
}
