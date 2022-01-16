using System;
using System.Collections.Generic;
using System.Text;

namespace EXP
{
    struct Position
    {
        public int x;
        public int y;

        public Position(int X, int Y)
        {
            x = X;
            y = Y;
        }

        public static bool operator ==(Position x, Position y)
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

        public static Position operator +(Position a, Position b)
        {
            Position pos = new Position(a.x + b.x, a.y + b.y);
            return pos;
        }
    }

    enum EDrawing
    {
        //墙壁
        wall,
        //正方形
        cube,
        //棍子
        stick,
        //坦克
        tank,
        //左小拐子
        left_small,
        //右小拐子
        right_small,
        //左长拐子
        left_long,
        //右长拐子
        right_long
    }

    class DrawingObject : IDraw
    {
        public Position pos;
        public EDrawing type;

        public DrawingObject()
        {

        }

        public DrawingObject(EDrawing type)
        {
            this.type = type;
        }

        public DrawingObject(EDrawing type, int x, int y)
        {
            pos = new Position(x, y);
            this.type = type;
        }

        public void Draw()
        {
            //屏幕外不用再绘制
            if (pos.y < 0)
                return;

            Console.SetCursorPosition(pos.x, pos.y);

            switch (type)
            {
                case EDrawing.wall:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case EDrawing.cube:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case EDrawing.stick:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case EDrawing.tank:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case EDrawing.left_small:
                case EDrawing.right_small:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case EDrawing.left_long:
                case EDrawing.right_long:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            Console.Write("■");
        }

        public void Clear()
        {
            //屏幕外不用再绘制
            if (pos.y < 0)
                return;
            Console.SetCursorPosition(pos.x, pos.y);
            Console.Write("  ");
        }
    }
}
