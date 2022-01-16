using System;
using System.Collections.Generic;
using System.Text;

namespace EXP
{
    //物体旋转方向
    enum E_Transform
    {
        Right,
        Left
    }

    class BlockWorker:IDraw
    {
        Dictionary<EDrawing, BlockInfo> dictionary;

        //现在生成的block信息，选择的种类
        BlockInfo nowBlockInfo;
        //现在生成的block的旋转种类
        public int nowBlockIndex;

        //每一个小方块
        List<DrawingObject> blocks;
        

        public BlockWorker()
        {
            //初始化 装块信息 
            dictionary = new Dictionary<EDrawing, BlockInfo>()
            {
                { EDrawing.cube, new BlockInfo(EDrawing.cube) },
                { EDrawing.stick, new BlockInfo(EDrawing.stick) },
                { EDrawing.tank, new BlockInfo(EDrawing.tank) },
                { EDrawing.left_small, new BlockInfo(EDrawing.left_small) },
                { EDrawing.right_small, new BlockInfo(EDrawing.right_small) },
                { EDrawing.left_long, new BlockInfo(EDrawing.left_long) },
                { EDrawing.right_long, new BlockInfo(EDrawing.right_long) },
            };

            //随机方块
            RandomCreateBlock();
        }

        //随机生成下落方块
        public void RandomCreateBlock()
        {
            Random random = new Random();
            EDrawing type = (EDrawing)random.Next(1,8);

            blocks = new List<DrawingObject>()
            {
                new DrawingObject(type),
                new DrawingObject(type),
                new DrawingObject(type),
                new DrawingObject(type),
            };

            blocks[0].pos = new Position(24, -5);

            nowBlockInfo = dictionary[type];
            nowBlockIndex = random.Next(0, nowBlockInfo.Count);
            Position[] pos = nowBlockInfo[nowBlockIndex];
            for (int i = 0; i < pos.Length; i++)
            {
                blocks[i + 1].pos = blocks[0].pos + pos[i];
            }

        }

        //绘制方块
        public void Draw()
        {
            for (int i = 0; i < blocks.Count; i++)
                blocks[i].Draw();
        }

        //擦除方块
        public void Clear()
        {
            for (int i = 0; i < blocks.Count; i++)
                blocks[i].Clear();
        }

        //能否旋转判断
        public bool CanChange(Map map, E_Transform toward)
        {
            //临时存储转向
            int tempIndex = nowBlockIndex;
            switch (toward)
            {
                case E_Transform.Right:
                    if (++tempIndex > nowBlockInfo.Count - 1)
                        tempIndex = 0;
                    break;
                case E_Transform.Left:
                    if (--tempIndex < 0)
                        tempIndex = nowBlockInfo.Count - 1;
                    break;
            }

            //修改block的位置
            Position[] movePos = nowBlockInfo[tempIndex];
            Position pos;

            //静态墙壁
            for (int i = 0; i < movePos.Length; i++)
            {
                pos = blocks[0].pos + movePos[i];
                //判断左右边界 和 下边界
                if (pos.x < 2 ||
                    pos.x >= Game.w - 2 ||
                    pos.y >= map.h)
                {
                    return false;
                }
            }

            //碰到动态墙壁
            for (int i = 0; i < blocks.Count - 1; i++)
            {
                pos = blocks[0].pos + movePos[i];
                for (int j = 0; j < map.dynamicWalls.Count; j++)
                {
                    if (pos == map.dynamicWalls[j].pos)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        //物体旋转
        public void ChangeBlockIndex(E_Transform toward)
        {
            //清楚之前画的东西
            Clear();
            //修改Index
            switch(toward)
            {
                case E_Transform.Right:
                    if (++nowBlockIndex > nowBlockInfo.Count - 1)
                        nowBlockIndex = 0;
                    break;
                case E_Transform.Left:
                    if (--nowBlockIndex < 0)
                        nowBlockIndex = nowBlockInfo.Count - 1;
                    break;
            }

            //修改block的位置
            Position[] pos = nowBlockInfo[nowBlockIndex];
            for (int i = 0; i < pos.Length; i++)
                blocks[i + 1].pos = blocks[0].pos + pos[i];
            Draw();
        }

        //物体是否停止运动
        public bool CanMove(Map map)
        {
            Position movePos = new Position(0, 1);
            Position pos;

            //静态墙壁，碰底
            for(int i = 0;i<blocks.Count;i++)
            {
                pos = blocks[i].pos + movePos;
                if(pos.y >= map.h)
                //碰到底了
                {
                    map.AddDynamicWalls(blocks);
                    RandomCreateBlock();
                    return false;
                }    
            }

            //碰到动态墙壁
            for(int i = 0;i<blocks.Count;i++)
            {
                pos = blocks[i].pos + movePos;
                for(int j = 0;j<map.dynamicWalls.Count;j++)
                {
                    if(pos == map.dynamicWalls[j].pos)
                    {
                        map.AddDynamicWalls(blocks);
                        RandomCreateBlock();
                        return false;
                    }
                }
            }
            return true;
        }

        //物体自动下落
        public void AutoMove()
        {
            //变位置之前擦除
            Clear();

            for (int i = 0; i < blocks.Count; i++)
            {
                //blocks[i].pos += downMove;
                blocks[i].pos.y += 1;
            }

            //变了位置再画
            Draw();
        }

        //物体是否能左、右移动
        public bool CanRLMove(Map map, E_Transform toward)
        {
            Position movePos = new Position(toward == E_Transform.Left ? -2 : 2, 0);
            //临时存放位置
            Position pos;

            //静态墙壁
            for (int i = 0; i < blocks.Count; i++)
            {
                pos = blocks[i].pos + movePos;
                //判断左右边界 和 下边界
                if (pos.x < 2 ||
                    pos.x >= Game.w - 2 )
                {
                    return false;
                }
            }

            //碰到动态墙壁
            for (int i = 0; i < blocks.Count; i++)
            {
                pos = blocks[i].pos + movePos;
                for (int j = 0; j < map.dynamicWalls.Count; j++)
                {
                    if (pos == map.dynamicWalls[j].pos)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //物体左、右移
        public void RLMove(E_Transform toward)
        {
            Clear();

            Position movePos = new Position(toward == E_Transform.Left ? -2 : 2, 0);

            for (int i = 0; i < blocks.Count; i++)
                blocks[i].pos += movePos;

            Draw();
        }
    }
}
