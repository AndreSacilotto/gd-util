using Godot;
using System;

namespace Util.Vector
{
    public static class Directions2D
    {
        public const int TOP = -1;
        public const int BOTTOM = 1;
        public const int LEFT = -1;
        public const int RIGHT = 1;

        /// <summary>x: 0, y: 0</summary>
        public static Vector2i None => new Vector2i(0, 0);

        /// <summary>x: 0, y: -1</summary>
        public static Vector2i Top => new Vector2i(0, TOP);
        /// <summary>x: 1, y: 0</summary>
        public static Vector2i Right => new Vector2i(RIGHT, 0);
        /// <summary>x: 0, y: 1</summary>
        public static Vector2i Bottom => new Vector2i(0, BOTTOM);
        /// <summary>x: -1, y: 0</summary>
        public static Vector2i Left => new Vector2i(LEFT, 0);

        /// <summary>x: -1, y: 1</summary>
        public static Vector2i BottomLeft => new Vector2i(LEFT, BOTTOM);
        /// <summary>x: 1, y: 1</summary>
        public static Vector2i BottomRight => new Vector2i(RIGHT, BOTTOM);
        /// <summary>x: -1, y: -1</summary>
        public static Vector2i TopLeft => new Vector2i(LEFT, TOP);
        /// <summary>x: 1, y: -1</summary>
        public static Vector2i TopRight => new Vector2i(RIGHT, TOP);

        public static bool IsDiagonal(Vector2i dir) => dir.x != 0 && dir.y != 0;
        public static bool IsStraight(Vector2i dir) => dir.x == 0 || dir.y == 0;

        public static Vector2i PositionToDirection(Vector2 position, Vector2 center = default)
        {
            var rad = Mathf.Atan2(center.y - position.y, center.x - position.x) + Mathf.Pi;
            return new Vector2i(Mathf.RoundToInt(Mathf.Cos(rad)), Mathf.RoundToInt(Mathf.Sin(rad)));
        }
        public static Vector2 PositionToFloatDirection(int decimals, Vector2 position, Vector2 center = default)
        {
            var rad = Mathf.Atan2(center.y - position.y, center.x - position.x) + Mathf.Pi;
            return new Vector2((float)Math.Round(Mathf.Cos(rad), decimals), (float)Math.Round(Mathf.Sin(rad), decimals));
        }
        public static Vector2 PositionToFloatDirection(Vector2 position, Vector2 center = default)
        {
            var rad = Mathf.Atan2(center.y - position.y, center.x - position.x) + Mathf.Pi;
            return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        }
        public static Vector2i PositionToDiagonal(Vector2 position, Vector2 center = default)
        {
            var rad = Mathf.Atan2(position.y - center.y, position.x - center.x);
            if (rad < 0)
            {
                if (rad >= -MathUtil.TAU_90)
                    return TopRight;
                return TopLeft;
            }
            else
            {
                if (rad <= MathUtil.TAU_90)
                    return BottomRight;
                return BottomLeft;
            }
        }

        public static Vector2i PositionToStraight(Vector2 position, Vector2 center = default)
        {
            var rad = Mathf.Pi - Mathf.Atan2(center.y - position.y, center.x - position.x);
            if (rad <= MathUtil.TAU_45)
                return Right;
            else if (rad <= MathUtil.TAU_45 * 3f)
                return Top;
            else if (rad <= MathUtil.TAU_45 * 5f)
                return Left;
            else if (rad <= MathUtil.TAU_45 * 7f)
                return Bottom;
            return Right;
        }

    }
}