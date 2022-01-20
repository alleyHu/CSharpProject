using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    /// <summary>
    /// 蛇身体类型枚举
    /// </summary>
    enum ESnakeBodyType
    {
        /// <summary>
        /// 蛇头
        /// </summary>
        head,
        /// <summary>
        /// 蛇身体
        /// </summary>
        body
    }

    class SnakeBody:GameObject
    {
        public ESnakeBodyType type;

        public SnakeBody(ESnakeBodyType type, int x, int y)
        {
            position = new Position(x, y);
            this.type = type;
        }

        public override void Draw()
        {
            Console.SetCursorPosition(position.x, position.y);
            Console.ForegroundColor = type == ESnakeBodyType.head ? ConsoleColor.Yellow : ConsoleColor.Green;
            Console.Write(type == ESnakeBodyType.head ? "●" : "◎");
        }
    }
}
