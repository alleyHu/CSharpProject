using System;
using System.Collections.Generic;
using System.Text;

namespace EXP
{
    class BlockInfo
    {
        public List<Position[]> list;

        public BlockInfo(EDrawing type)
        {
            //必须要初始化才能往里面装东西
            list = new List<Position[]>();

            switch (type)
            {
                case EDrawing.cube:
                    //添加了一个形状的位置信息
                    list.Add(new Position[3] {
                        new Position(2,0),
                        new Position(0,1),
                        new Position(2,1)
                    });
                    break;
                case EDrawing.stick:
                    //初始化 长条形状的4种形态的坐标信息
                    list.Add(new Position[3] {
                        new Position(0,-1),
                        new Position(0,1),
                        new Position(0,2)
                    });
                    list.Add(new Position[3] {
                        new Position(-4,0),
                        new Position(-2,0),
                        new Position(2,0)
                    });
                    list.Add(new Position[3] {
                        new Position(0,-2),
                        new Position(0,-1),
                        new Position(0,1)
                    });
                    list.Add(new Position[3] {
                        new Position(-2,0),
                        new Position(2,0),
                        new Position(4,0)
                    });
                    break;
                case EDrawing.tank:
                    list.Add(new Position[3] {
                        new Position(-2,0),
                        new Position(2,0),
                        new Position(0,1)
                    });
                    list.Add(new Position[3] {
                        new Position(0,-1),
                        new Position(-2,0),
                        new Position(0,1)
                    });
                    list.Add(new Position[3] {
                        new Position(0,-1),
                        new Position(-2,0),
                        new Position(2,0)
                    });
                    list.Add(new Position[3] {
                        new Position(0,-1),
                        new Position(2,0),
                        new Position(0,1)
                    });
                    break;
                case EDrawing.left_small:
                    list.Add(new Position[3]{
                        new Position(0,-1),
                        new Position(2,0),
                        new Position(2,1)
                    });
                    list.Add(new Position[3]{
                        new Position(2,0),
                        new Position(0,1),
                        new Position(-2,1)
                    });
                    list.Add(new Position[3]{
                       new Position(-2,-1),
                        new Position(-2,0),
                        new Position(0,1)
                    });
                    list.Add(new Position[3]{
                        new Position(0,-1),
                        new Position(2,-1),
                        new Position(-2,0)
                    });
                    break;
                case EDrawing.right_small:
                    list.Add(new Position[3]{
                        new Position(0,-1),
                        new Position(-2,0),
                        new Position(-2,1)
                    });
                    list.Add(new Position[3]{
                        new Position(-2,-1),
                        new Position(0,-1),
                        new Position(2,0)
                    });
                    list.Add(new Position[3]{
                        new Position(2,-1),
                        new Position(2,0),
                        new Position(0,1)
                    });
                    list.Add(new Position[3]{
                        new Position(0,1),
                        new Position(2,1),
                        new Position(-2,0)
                    });
                    break;
                case EDrawing.left_long:
                    list.Add(new Position[3]{
                        new Position(-2,-1),
                        new Position(0,-1),
                        new Position(0,1)
                    });
                    list.Add(new Position[3]{
                        new Position(2,-1),
                        new Position(-2,0),
                        new Position(2,0)
                    });
                    list.Add(new Position[3]{
                        new Position(0,-1),
                        new Position(2,1),
                        new Position(0,1)
                    });
                    list.Add(new Position[3]{
                        new Position(2,0),
                        new Position(-2,0),
                        new Position(-2,1)
                    });
                    break;
                case EDrawing.right_long:
                    list.Add(new Position[3]{
                        new Position(0,-1),
                        new Position(0,1),
                        new Position(2,-1)
                    });
                    list.Add(new Position[3]{
                        new Position(2,0),
                        new Position(-2,0),
                        new Position(2,1)
                    });
                    list.Add(new Position[3]{
                        new Position(0,-1),
                        new Position(-2,1),
                        new Position(0,1)
                    });
                    list.Add(new Position[3]{
                        new Position(-2,-1),
                        new Position(-2,0),
                        new Position(2,0)
                    });
                    break;
                default:
                    break;
            }
        }

        public Position[] this[int index]
        {
            get
            {
                if (index < 0)
                    index = 0;
                else if (index > list.Count - 1)
                    index = list.Count - 1;
                return list[index];
            }
        }

        public int Count
        {
            get => list.Count;
        }
    }
}
