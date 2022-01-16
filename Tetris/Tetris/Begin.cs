using System;
using System.Collections.Generic;
using System.Text;

namespace EXP
{
    class Begin : StartOrEnd
    {
        public Begin()
        {
            selectID = 0;
            title = "俄罗斯方块";
            firstOption = "开始游戏";
            secondOption = "退出游戏";
        }

        public override void EnterFDoSomthing()
        {
            if (selectID == 0)
                Game.ChangeScene(ESceneType.Play);
            else
                Environment.Exit(0);
        }
    }
}
