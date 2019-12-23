using UnityEngine;

namespace TEDinc.Utils
{
    public class Int2
    {
        public int x, y;

        public Int2()
        {
            x = 0;
            y = 0;
        }
        public Int2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator *(Int2 i, Vector2 v)
        {
            return new Vector2(v.x * (float)i.x, v.y * (float)i.y);
        }
        public static Vector2 operator *(Vector2 v, Int2 i)
        {
            return new Vector2(v.x * (float)i.x, v.y * (float)i.y);
        }
        public static Int2 operator -(Int2 i, int a)
        {
            return new Int2(i.x - a, i.y - a);
        }
    }
}