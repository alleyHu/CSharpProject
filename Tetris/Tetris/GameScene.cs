using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EXP
{
    class GameScene : IUpdate
    {
        Map map;
        BlockWorker worker;

        public GameScene()
        {
            map = new Map(this);
            worker = new BlockWorker();
            InputThread.Instance.action += Input;
        }

        public void Update()
        {
            lock (worker)
            {
                map.Draw();
                worker.Draw();
                if (worker.CanMove(map))
                    worker.AutoMove();
            }
            Thread.Sleep(200);
        }

        public void Input()
        {
            if(Console.KeyAvailable)
                {
                    lock (worker)
                    {

                        switch (Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.LeftArrow:
                                if(worker.CanChange(map,E_Transform.Left))
                                    worker.ChangeBlockIndex(E_Transform.Left);
                                break;
                            case ConsoleKey.RightArrow:
                                if (worker.CanChange(map, E_Transform.Right))
                                    worker.ChangeBlockIndex(E_Transform.Right);
                                break;
                            case ConsoleKey.A:
                                if (worker.CanRLMove(map, E_Transform.Left))
                                    worker.RLMove(E_Transform.Left);
                                break;
                            case ConsoleKey.D:
                                if (worker.CanRLMove(map, E_Transform.Right))
                                    worker.RLMove(E_Transform.Right);
                                break;
                            case ConsoleKey.S:
                                if (worker.CanMove(map))
                                    worker.AutoMove();
                                break;
                        }
                    }
                }

        }

        public void StopThread()
        {
            InputThread.Instance.action -= Input;
        }
    }
}
