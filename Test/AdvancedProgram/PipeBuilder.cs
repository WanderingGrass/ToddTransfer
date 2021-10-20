using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AdvancedProgram
{
    /// <summary>
    /// 模拟Middleware
    /// </summary>
    public class PipeBuilder
    {
         Action<string> _mainAction;
         List<Type> _pipeTypes;
        public PipeBuilder(Action<string> mainAction)
        {
            _mainAction = mainAction;
            _pipeTypes = new List<Type>();
        }
        public void AddPipe(Type pipeType)
        {
            if (!pipeType.GetTypeInfo().IsInstanceOfType(typeof(Pipe)))
            {
                throw new Exception();
            }
            _pipeTypes.Add(pipeType);
        }
        private Action<string> CreatePipe(int index)
        {
            if (index < _pipeTypes.Count - 1)
            {
                var childerPipeHandle = CreatePipe(index + 1);
                var pipe = (Pipe)Activator.CreateInstance(_pipeTypes[index+1], childerPipeHandle);
                return pipe.Handler;
            }
            else
            {
                var finalPipe = (Pipe)Activator.CreateInstance(_pipeTypes[index], _mainAction);
                return finalPipe.Handler;
            }
        }
        public Action<string> Build()
        {
            return CreatePipe(0);
        }
    }
    public abstract class Pipe
    {
        protected Action<string> _action;
        public Pipe(Action<string> action)
        {
            _action = action;
        }
        public abstract void Handler(string msg);
    }
    public class Wrap : Pipe
    {
        public Wrap(Action<string> action) : base(action) { }
        public override void Handler(string msg)
        {
           Console.WriteLine("starting");
            _action(msg);
            Console.WriteLine("end");
        }
    }
    public class Try : Pipe
    {
        public Try(Action<string> action) : base(action) { }
        public override void Handler(string msg)
        {
            Console.WriteLine("trying");
            _action(msg);
        }
    }
}
