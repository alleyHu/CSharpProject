using System;
using System.Collections.Generic;
using System.Text;

namespace EXP
{
    class Map
    {
        //控制台大小信息
        public int w;
        public int h;

        //墙壁信息
        //固定墙壁
        public List<DrawingObject> walls = new List<DrawingObject>();
        //动态墙壁
        public List<DrawingObject> dynamicWalls = new List<DrawingObject>();

        //记录每一行有多少个小方块的容器
        //索引对应的就是行号
        private int[] recordInfo;

        private GameScene nowGameScene;

        public Map(GameScene gameScene)
        {
            nowGameScene = gameScene;

            h = Game.h - 6;
            //这个就代表对应每行的计数初始化 默认都为0
            //0~Game.h-7
            recordInfo = new int[h];

            w = 0;
            //在绘制 横向的固定墙壁
            for (int i = 0; i < Game.w; i += 2)
            {
                walls.Add(new DrawingObject(EDrawing.wall, i, h));
                ++w;
            }
            w -= 2;

            for (int i = 0; i < h; i++)
            {
                walls.Add(new DrawingObject(EDrawing.wall, 0, i));
                walls.Add(new DrawingObject(EDrawing.wall, Game.w - 2, i));
            }
        }

        public void Draw()
        {
            for (int i = 0; i < walls.Count; i++)
                walls[i].Draw();
            for (int i = 0; i < dynamicWalls.Count; i++)
                dynamicWalls[i].Draw();
        }

        //清除动态墙
        public void Clear()
        {
            for (int i = 0; i < dynamicWalls.Count; i++)
                dynamicWalls[i].Clear();
        }

        //添加动态墙
        public void AddDynamicWalls(List<DrawingObject> walls)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                dynamicWalls.Add(walls[i]);

                if (walls[i].pos.y <= 0)
                //碰到顶部，游戏结束
                {                   
                    nowGameScene.StopThread();
                    Game.ChangeScene(ESceneType.End);
                    return;
                }

                recordInfo[walls[i].pos.y] += 1;
            }

            //检测有没有移除
            Clear();
            CheckLayer();
            Draw();
        }

        //检测某一层是否满了
        private bool CheckLayer()
        {
            bool flag = false;
            List<DrawingObject> delList = new List<DrawingObject>();

            for(int i = 0;i< recordInfo.Length; i++)
            {
                //一层满了
                if(recordInfo[i] == w)
                {
                    //这一行的所有小方块移除
                    for(int j = 0; j<dynamicWalls.Count;j++)
                    {
                        if (dynamicWalls[j].pos.y == i)
                            delList.Add(dynamicWalls[j]);
                        else if(dynamicWalls[j].pos.y < i)
                            ++dynamicWalls[j].pos.y;
                    }
                    for (int j = 0; j < delList.Count; j++)
                        dynamicWalls.Remove(delList[j]);
                    //记录小方块数量的数组从上到下迁移
                    for (int j = i; j > 0; j--)
                        recordInfo[j] = recordInfo[j - 1];
                    recordInfo[0] = 0;
                    flag = true;
                    
                }
            }

            return flag;
        }
    }
}
