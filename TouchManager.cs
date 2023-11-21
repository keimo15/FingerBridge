using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// タッチ判定

public class TouchManager : MonoBehaviour, IPointerClickHandler
{
    bool isBlack = true;

    // Start is called before the first frame update
    void Start()
    {
        isBlack = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TouchBlock();
        isBlack = !isBlack;
    }

    void TouchBlock()
    {
        DrowBlock block = gameObject.GetComponent<DrowBlock>();
        if (isBlack)
        {
            block.ToWhiteBlock();
        }
        else
        {
            block.ToBlackBlock();
        }
    }
}
