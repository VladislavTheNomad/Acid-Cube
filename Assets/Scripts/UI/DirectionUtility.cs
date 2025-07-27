using UnityEngine;

namespace AcidCube
{
        public enum Direction
        {
            Right,
            Left,
            Up,
            Down,
            Forward,
            Backward,
        }
    public static class DirectionUtils
    {
        public static int ToSign(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    return 1;
                case Direction.Left:
                    return -1;
                case Direction.Up:
                    return 1;
                case Direction.Forward:
                    return 1;
                case Direction.Backward:
                    return -1;
                default:
                    return 0;
            }
        }

        public static Vector3 ToVector(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    return Vector3.left;
                case Direction.Left:
                    return Vector3.right;
                case Direction.Up:
                    return Vector3.up;
                case Direction.Down:
                    return Vector3.down;
                default:
                    return Vector3.zero;
            }
        }
    }

}
