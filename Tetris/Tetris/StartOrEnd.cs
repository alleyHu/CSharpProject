using System;
using System.Collections.Generic;
using System.Text;

namespace EXP
{
    abstract class StartOrEnd:IUpdate
    {
        //当前选择了哪个选项
        public int selectID;
        //显示标题
        public string title;
        //选项一
        public string firstOption;
        //选项二
        public string secondOption;

        public abstract void EnterFDoSomthing();

        public StartOrEnd()
        {
            selectID = 0;
        }

        public void Update()
        {
            //开始和结束场景的 游戏逻辑 
            //选择当前的选项 然后 监听 键盘输入 wsj
            Console.ForegroundColor = ConsoleColor.White;
            //显示标题
            Console.SetCursorPosition(Game.w / 2 - title.Length, 5);
            Console.Write(title);
            //显示下方的选项
            Console.SetCursorPosition(Game.w / 2 - firstOption.Length, 8);
            Console.ForegroundColor = selectID == 0 ? ConsoleColor.Red : ConsoleColor.White;
            Console.Write(firstOption);
            Console.SetCursorPosition(Game.w / 2 - 4, 10);
            Console.ForegroundColor = selectID == 1 ? ConsoleColor.Red : ConsoleColor.White;
            Console.Write(secondOption);
            //检测输入
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                    --selectID;
                    if (selectID < 0)
                    {
                        selectID = 0;
                    }
                    break;
                case ConsoleKey.S:
                    ++selectID;
                    if (selectID > 1)
                    {
                        selectID = 1;
                    }
                    break;
                case ConsoleKey.F:
                    EnterFDoSomthing();
                    break;
            }
        }
    }

}
