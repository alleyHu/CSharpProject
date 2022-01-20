using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class GameScene : IUpdate
    {
        public TheSnake snake;
        public Map map;
        public Food food;

        //计数
        private int index = 0;

        public GameScene()
        {
            snake = new TheSnake();
            map = new Map();
            food = new Food(snake);
        }

        public void Update()
        {
            if(index % 9000 == 0)
            {
                index = 0;
                map.Draw();
                food.Draw();

                snake.Move();
                snake.Draw();

                if (snake.IsDie(map))
                {
                    Game.ChangeScene(ESceneType.End);
                }
                snake.Eat(food);
                
               
            }
            ++index;
            
            //WASD移动
            if(Console.KeyAvailable)
            {
                switch(Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                        snake.ChangeDir(EDir.up);
                        break;
                    case ConsoleKey.S:
                        snake.ChangeDir(EDir.down);
                        break;
                    case ConsoleKey.A:
                        snake.ChangeDir(EDir.left);
                        break;
                    case ConsoleKey.D:
                        snake.ChangeDir(EDir.right);
                        break;
                }
            }
        }
    }
}
