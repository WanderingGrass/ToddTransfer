using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdvancedProgram
{
  /// <summary>
  /// 管道实现
  /// </summary>
  /// <typeparam name="T"></typeparam>
    public interface IWrite<T>
    {
        public void Push(T msg);
        void Complete();
    }
    public interface IRead<T>
    {
        Task<T> Read();
        bool IsComplete();
        
    }
    public class Channels<T> : IWrite<T>, IRead<T>
    {
        private bool Finished;
        private ConcurrentQueue<T> _queue;
        private SemaphoreSlim _flag;
        public Channels()
        {
            _queue = new ConcurrentQueue<T>();
            _flag = new SemaphoreSlim(0);
        }

        public void Push(T msg)
        {
            _queue.Enqueue(msg);
            _flag.Release();
        }

        public async Task<T> Read()
        {
            await _flag.WaitAsync();
            if (_queue.TryDequeue(out var msg))
            {
                return msg;
            }
            return default;
        }
        public void Complete()
        {
            Finished = true;
        }

        public bool IsComplete()
        {
            return Finished&& _queue.IsEmpty;
        }
    }
}
