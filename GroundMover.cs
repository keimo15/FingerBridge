using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Right,
    Left,
    Up,
    Down,
}

public class GroundMover : MonoBehaviour
{
    Rigidbody2D rbody;

    public Direction direction;                 // 移動方向
    public float speed;                         // 移動速度
    public float moveDistance;                  // 移動する距離
    float distance;                             // 移動距離

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        distance += speed * Time.deltaTime;
        switch (direction)
        {
          case Direction.Right:
            rbody.velocity = new Vector2(speed, 0);
            if (distance >= moveDistance)
            {
                direction = Direction.Left;
                distance = 0;
            }
            break;
          case Direction.Left:
            rbody.velocity = new Vector2(-speed, 0);
            if (distance >= moveDistance)
            {
                direction = Direction.Right;
                distance = 0;
            }
            break;
          case Direction.Up:
            rbody.velocity = new Vector2(0, speed);
            if (distance >= moveDistance)
            {
                direction = Direction.Down;
                distance = 0;
            }
            break;
          case Direction.Down:
            rbody.velocity = new Vector2(0, -speed);
            if (distance >= moveDistance)
            {
                direction = Direction.Up;
                distance = 0;
            }
            break;
        }
    }
}
