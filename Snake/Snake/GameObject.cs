using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    interface IDraw
    {
        public void Draw();
    }

    #region 位置结构体
    struct Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator==(Position x, Position y)
        {
            if (x.x == y.x && x.y == y.y)
                return true;
            else
                return false;
        }

        public static bool operator !=(Position x, Position y)
        {
            if (x.x == y.x && x.y == y.y)
                return false;
            else
                return true;
        }
    }
    #endregion

    abstract class GameObject : IDraw
    {
        public Position position;

        public abstract void Draw();
    }
}
