using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EXP
{
    class InputThread
    {
        public event Action action;

        private Thread thread;

        #region 单例模式
        private static InputThread instance = new InputThread();

        private InputThread() 
        {
            thread = new Thread(InputCheck);
            thread.IsBackground = true;
            thread.Start();
        }

        public static InputThread Instance
        {
            get => instance;
        }
        #endregion

        private void InputCheck()
        {
            while (true)
                action?.Invoke();
        }

    }
}
