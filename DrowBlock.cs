using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrowBlock : MonoBehaviour, IPointerClickHandler
{
    public bool isBlack;                                // ブロックが黒であるか
    public int positionX;                               // ブロックの x 座標
    public int positionY;                               // ブロックの y 座標
    [SerializeField] public GameObject[] triangles;     // 子要素の三角形, 0:上, 1:左, 2:下, 3:右
    [SerializeField] BlockManager blockManager;

    string moveObjectTagName = "MoveObject";
    public bool onTriggerObject = false;

    void Start()
    {
        isBlack = true;
        blockManager.AddBlockManagerArray(this);
        onTriggerObject = false;
    }

    // タッチされた判定
    public void OnPointerClick(PointerEventData eventData)
    {
        // オブジェクトと重なっているブロックの色は変えられない
        if (!onTriggerObject) blockManager.ChangeBlockColor(this);
    }

    // オブジェクトとの接触判定
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == moveObjectTagName)
        {
            onTriggerObject = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == moveObjectTagName)
        {
            onTriggerObject = false;
        }
    }
}
