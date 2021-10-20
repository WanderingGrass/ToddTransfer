// See https://aka.ms/new-console-template for more information

using MultithreadingDemo;

Console.WriteLine("Hello, World!");
SequentialRead s = new SequentialRead();
//Semaphore Semaphore = new Semaphore(1, 1);
//Thread a = new Thread(new ThreadStart(s.SequentialReadRunA));
//a.Priority = ThreadPriority.Highest;
//Thread l = new Thread(new ThreadStart(s.SequentialReadRunL));
//l.Priority = ThreadPriority.Normal;
//Thread i = new Thread(new ThreadStart(s.SequentialReadRunI));
//i.Priority = ThreadPriority.Lowest;

//i.Start();
//a.Start();
//l.Start();
while (true)
{
    ThreadPool.QueueUserWorkItem(s.SequentialReadRunA);
    ThreadPool.QueueUserWorkItem(s.SequentialReadRunL);
    ThreadPool.QueueUserWorkItem(s.SequentialReadRunI);
    Thread.Sleep(1000);
}







