using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    enum EDir
    {
        left,
        right,
        up,
        down
    }

    class TheSnake : IDraw
    {
        public SnakeBody[] snakeBodies;
        //目前蛇的身体数
        public int size;
        //蛇头的朝向
        EDir dir;

        public TheSnake()
        {
            snakeBodies = new SnakeBody[200];
            snakeBodies[0] = new SnakeBody(ESnakeBodyType.head, 6, 6);
            size = 1;
            dir = EDir.right;
        }

        public void Draw()
        {
            for(int i = 0; i < size; i++)
            {
                snakeBodies[i].Draw();
            }
        }

        public void Move()
        {
            //清空蛇尾的图标
            Console.SetCursorPosition(snakeBodies[size - 1].position.x, snakeBodies[size - 1].position.y);
            Console.Write("  ");

            //后续身体的移动：继承上一个的位置
            for (int i = size - 1; i > 0; i--)
            {
                snakeBodies[i].position = snakeBodies[i - 1].position;
            }

            switch (dir)
            {
                case EDir.left:
                    snakeBodies[0].position.x -= 2;
                    break;
                case EDir.right:
                    snakeBodies[0].position.x += 2;
                    break;
                case EDir.up:
                    snakeBodies[0].position.y --;
                    break;
                case EDir.down:
                    snakeBodies[0].position.y ++;
                    break;
            }           
        }

        public void ChangeDir(EDir dir)
        {
            //只有头部的时候 可以直接左转右 右转左  上转下 下转上
            //有身体时 这种情况就不能直接转、
            if (dir == this.dir ||
                size > 1 &&
                (this.dir == EDir.left && dir == EDir.right ||
                 this.dir == EDir.right && dir == EDir.left ||
                 this.dir == EDir.up && dir == EDir.down ||
                 this.dir == EDir.down && dir == EDir.up))
            {
                return;
            }

            //只要没有return 就记录外面传入的方向 之后就会按照这个方向去移动
            this.dir = dir;
        }

        public void AddBody()
        {
            //长身体
            SnakeBody fromtbody = snakeBodies[size - 1];
            snakeBodies[size] = new SnakeBody(ESnakeBodyType.body,fromtbody.position.x,fromtbody.position.y);

            ++size;
        }

        public void Eat(Food food)
        {
            if (food.position == snakeBodies[0].position)
            {
                //食物随机生成
                food.RandomPos(this);
                food.Draw();
                //蛇长身体
                AddBody();
            }
        }

        public bool IsDie(Map map)
        {
            //撞到了墙
            for(int i = 0;i<map.walls.Length;i++)
            {
                if (snakeBodies[0].position == map.walls[i].position)
                    return true;
            }
            //撞到了身体
            for(int i = 1;i<size;i++)
            {
                if (snakeBodies[0].position == snakeBodies[i].position)
                    return true;
            }

            return false;
        }
    }
}
