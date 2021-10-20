using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingDemo
{
    /// <summary>
    ///有3个独立的线程，一个只会输出A，一个只会输出L，一个只会输出I。在三个线程同时启动的情况下，请用合理的方式让他们按顺序打印ALIALI。三个线程开始正常输出后，主线程若检测到用户任意的输入则停止三个打印线程的工作，整体退出。
    ///解决方案：
    ///1:利用Semaphore，因为它本身是负责协调各个线程
    ///2：利用优先级
    ///3：lock
    ///4: ThreadPool  QueueUserWorkItem
    /// </summary>
    public class SequentialRead
    {
        public async void SequentialReadRunA(Object stateInfo)
        {
            Console.WriteLine("A");
        }
        public async void SequentialReadRunL(Object stateInfo)
        {
            Console.WriteLine("L");
        }
        public async void SequentialReadRunI(Object stateInfo)
        {
            Console.WriteLine("I");
        }
    }
}
