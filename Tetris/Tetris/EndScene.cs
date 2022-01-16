using System;
using System.Collections.Generic;
using System.Text;

namespace EXP
{
    class EndScene : StartOrEnd
    {
        public EndScene()
        {
            selectID = 0;
            title = "游戏结束";
            firstOption = "返回游戏主菜单";
            secondOption = "退出游戏";
        }

        public override void EnterFDoSomthing()
        {
            if (selectID == 0)
                Game.ChangeScene(ESceneType.Start);
            else
                Environment.Exit(0);
        }
    }
}
