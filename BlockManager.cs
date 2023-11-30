using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    static int rows = 5;                                        // 横に何ブロックあるか
    static int columns = 10;                                    // 縦に何ブロックあるか
    DrowBlock[,] drowBlocks = new DrowBlock[rows, columns];     // DrowBlock 全て
    Queue<DrowBlock> whiteBlocks = new Queue<DrowBlock>();
    public int maxWhiteBlocksNum = 5;

    string blackGroundLayerName = "BlackGround";        // 黒の地面判定
    string whiteGroundLayerName = "WhiteGround";        // 白の地面判定

    // DrowBlocks に DrowBlock を追加する
    public void AddBlockManagerArray(DrowBlock drowBlock)
    {
        int indexX = drowBlock.positionX;
        int indexY = drowBlock.positionY;
        drowBlocks[indexX, indexY] = drowBlock;
    }

    // 三角形単位で色と当たり判定を変える
    public void ChangeTriangleColor(GameObject triangle, bool newColorIsBlack)
    {
        if (newColorIsBlack)
        {
            triangle.GetComponent<SpriteRenderer>().color = Color.black;
            triangle.layer = LayerMask.NameToLayer(blackGroundLayerName);
        }
        else
        {
            triangle.GetComponent<SpriteRenderer>().color = Color.white;
            triangle.layer = LayerMask.NameToLayer(whiteGroundLayerName);
        }
    }

    // タッチされたブロックとその周辺のブロックの判定を変える
    public void ChangeBlockColor(DrowBlock block)
    {
        block.isBlack = !block.isBlack;
        ColorSpread(drowBlocks[block.positionX, block.positionY]);
        // 上のブロックが白なら
        if (block.positionY < columns - 1 && !drowBlocks[block.positionX, block.positionY+1].isBlack)
            ColorSpread(drowBlocks[block.positionX, block.positionY+1]);
        // 左のブロックが白なら
        if (block.positionX > 0           && !drowBlocks[block.positionX-1, block.positionY].isBlack)
            ColorSpread(drowBlocks[block.positionX-1, block.positionY]);
        // 下のブロックが白なら
        if (block.positionY > 0           && !drowBlocks[block.positionX, block.positionY-1].isBlack)
            ColorSpread(drowBlocks[block.positionX, block.positionY-1]);
        // 右のブロックが白なら
        if (block.positionX < rows - 1    && !drowBlocks[block.positionX+1, block.positionY].isBlack)
            ColorSpread(drowBlocks[block.positionX+1, block.positionY]);

        UpdateQueue(block);
    }

    // 周囲のブロックの色に応じて、ブロック内のどの三角形の色を変えるか判定する
    void ColorSpread(DrowBlock block)
    {        
        // 黒にするときは常に四角単位で色が変わる
        if (block.isBlack)
        {
            foreach(GameObject triangle in block.triangles) ChangeTriangleColor(triangle, block.isBlack);
            return;
        }

        // 左と上が一致 && 右と下が不一致 -> (三角形) 左と上を変更
        if (   ( block.positionX-1 < 0         || SameColor(block, drowBlocks[block.positionX-1, block.positionY]) )
            && ( block.positionY+1 > columns-1 || SameColor(block, drowBlocks[block.positionX, block.positionY+1]) )
            && ( block.positionX+1 > rows-1    || NotSameColor(block, drowBlocks[block.positionX+1, block.positionY]) )
            && ( block.positionY-1 < 0         || NotSameColor(block, drowBlocks[block.positionX, block.positionY-1]) ))
        {
            ChangeTriangleColor(block.triangles[0], block.isBlack);
            ChangeTriangleColor(block.triangles[1], block.isBlack);
            ChangeTriangleColor(block.triangles[2], !block.isBlack);
            ChangeTriangleColor(block.triangles[3], !block.isBlack);
            return;
        }

        // 左と下が一致 && 右と上が不一致 -> (三角形) 左と下を変更
        if (   ( block.positionX-1 < 0         || SameColor(block, drowBlocks[block.positionX-1, block.positionY]) )
            && ( block.positionY-1 < 0         || SameColor(block, drowBlocks[block.positionX, block.positionY-1]) )
            && ( block.positionX+1 > rows-1    || NotSameColor(block, drowBlocks[block.positionX+1, block.positionY]) )
            && ( block.positionY+1 > columns-1 || NotSameColor(block, drowBlocks[block.positionX, block.positionY+1]) ))
        {
            ChangeTriangleColor(block.triangles[1], block.isBlack);
            ChangeTriangleColor(block.triangles[2], block.isBlack);
            ChangeTriangleColor(block.triangles[0], !block.isBlack);
            ChangeTriangleColor(block.triangles[3], !block.isBlack);
            return;
        }

        // 右と下が一致 && 左と上が不一致 -> (三角形) 右と下を変更
        if (   ( block.positionX+1 > rows-1    || SameColor(block, drowBlocks[block.positionX+1, block.positionY]) )
            && ( block.positionY-1 < 0         || SameColor(block, drowBlocks[block.positionX, block.positionY-1]) )
            && ( block.positionX-1 < 0         || NotSameColor(block, drowBlocks[block.positionX-1, block.positionY]) )
            && ( block.positionY+1 > columns-1 || NotSameColor(block, drowBlocks[block.positionX, block.positionY+1]) ))
        {
            ChangeTriangleColor(block.triangles[2], block.isBlack);
            ChangeTriangleColor(block.triangles[3], block.isBlack);
            ChangeTriangleColor(block.triangles[0], !block.isBlack);
            ChangeTriangleColor(block.triangles[1], !block.isBlack);
            return;
        }

        // 右と上が一致 && 左と下が不一致 -> (三角形) 右と上を変更
        if (   ( block.positionX+1 > rows-1    || SameColor(block, drowBlocks[block.positionX+1, block.positionY]) )
            && ( block.positionY+1 > columns-1 || SameColor(block, drowBlocks[block.positionX, block.positionY+1]) )
            && ( block.positionX-1 < 0         || NotSameColor(block, drowBlocks[block.positionX-1, block.positionY]) )
            && ( block.positionY-1 < 0         || NotSameColor(block, drowBlocks[block.positionX, block.positionY-1]) ))
        {
            ChangeTriangleColor(block.triangles[0], block.isBlack);
            ChangeTriangleColor(block.triangles[3], block.isBlack);
            ChangeTriangleColor(block.triangles[1], !block.isBlack);
            ChangeTriangleColor(block.triangles[2], !block.isBlack);
            return;
        }

        // 上記以外は四角単位で色を変える
        foreach(GameObject triangle in block.triangles) ChangeTriangleColor(triangle, block.isBlack);;
    }

    void UpdateQueue(DrowBlock block)
    {
        // 色の変更後が黒なら queue から取り出す
        if (block.isBlack)
        {
            int count = whiteBlocks.Count;
            while (count > 0)
            {
                DrowBlock dequeueBlock = whiteBlocks.Dequeue();
                if (dequeueBlock != block){
                    whiteBlocks.Enqueue(dequeueBlock);
                    Debug.Log("Enqueue x:" + dequeueBlock.positionX + " y:" + dequeueBlock.positionY);
                }
                count--;
            }
        }

        // 色の変更後が白なら queue に加える
        else
        {
            whiteBlocks.Enqueue(block);
            Debug.Log("Enqueue x:" + block.positionX + " y:" + block.positionY);
            // queue の要素数が一定値を超えたら、古いものから黒に戻す
            if (whiteBlocks.Count > maxWhiteBlocksNum)
            {
                DrowBlock dequeueBlock = whiteBlocks.Dequeue();
                Debug.Log("Dequeue x:" + dequeueBlock.positionX + " y:" + dequeueBlock.positionY);
                ChangeBlockColor(dequeueBlock);
            }
        }
    }

    bool SameColor(DrowBlock block1, DrowBlock block2)
    {
        return block1.isBlack == block2.isBlack;
    }

    bool NotSameColor(DrowBlock block1, DrowBlock block2)
    {
        return block1.isBlack != block2.isBlack;
    }
}
