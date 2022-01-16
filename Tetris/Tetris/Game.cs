using System;
using System.Collections.Generic;
using System.Text;

namespace EXP
{
    #region 场景类型枚举
    /// <summary>
    /// 场景类型枚举
    /// </summary>
    enum ESceneType
    {
        /// <summary>
        /// 开始场景
        /// </summary>
        Start,
        /// <summary>
        /// 游戏主场景
        /// </summary>
        Play,
        /// <summary>
        /// 游戏结束场景
        /// </summary>
        End
    }
    #endregion

    #region 游戏类
    /// <summary>
    /// 游戏类
    /// </summary>
    class Game
    {
        //控制台宽高
        public static int w = 50;
        public static int h = 35;
        //当前游戏场景
        public static IUpdate SceneID;

        public Game()
        {
            Console.CursorVisible = false;

            Console.SetWindowSize(w,h);
            Console.SetBufferSize(w, h);

            ChangeScene(ESceneType.Start);
        }

        /// <summary>
        /// 游戏主循环
        /// </summary>
        public void MainLoad()
        {
            while(true)
            {
                if(SceneID != null)
                {
                    SceneID.Update();
                }
            }
        }

        /// <summary>
        /// 游戏场景切换
        /// </summary>
        public static void ChangeScene(ESceneType sceneType)
        {
            Console.Clear();
            switch (sceneType)
            {
                case ESceneType.Start:
                    SceneID = new Begin();
                    break;
                case ESceneType.Play:
                    SceneID = new GameScene();
                    break;
                case ESceneType.End:
                    SceneID = new EndScene();
                    break;
            }
        }
    }

    //游戏帧更新接口
    interface IUpdate
    {
        public void Update();
    }

    #endregion
}
