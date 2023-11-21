using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 三角形の情報

public enum Position
{
    Up,
    Left,
    Down,
    Right,
}

public class DrowTriangle : MonoBehaviour
{
    public Position directionPosition;
    public BW bw;
}
