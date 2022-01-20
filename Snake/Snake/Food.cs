using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class Food : GameObject
    {
        public Food(TheSnake snake)
        {
            RandomPos(snake);
        }
        public override void Draw()
        {
            Console.SetCursorPosition(position.x, position.y);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("¤");
        }
        
        //食物的随机位置，与蛇有关
        public void RandomPos(TheSnake snake)
        {
            Random r = new Random();
            position.x = r.Next(2, Game.w / 2 - 2) * 2;
            position.y = r.Next(1, Game.h  - 4);

            if(IsInBody(snake))
            {
                RandomPos(snake);
            }
        }

        //食物是否生成到了蛇身体部位
        private bool IsInBody(TheSnake snake)
        {
            for(int i = 0;i<snake.size;i++)
            {
                if (position == snake.snakeBodies[i].position)
                    return true;
            }
            return false;
        }
    }
}
