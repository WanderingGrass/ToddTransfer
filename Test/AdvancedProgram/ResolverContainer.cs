using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedProgram
{

    /// <summary>
    /// 依赖注入。生命周期 单例瞬态
    /// </summary>
    public class DependencyResolver
    {
        DependencyContainer _container;
        public DependencyResolver(DependencyContainer container)
        {
            _container = container;
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }
        public object GetService(Type type) {
            var dependency = _container.GetDependency(type);
            var contructor = dependency.Type.GetConstructors().Single();
            var parameters = contructor.GetParameters().ToArray();
            if (parameters.Length > 0)
            {
                var parameterImplementation = new Object[parameters.Length];
                for (int i = 0; i < parameters.Length; i++)
                {
                    parameterImplementation[i] = GetService(parameters[i].ParameterType);
                }
                return CreateImplementaiton(dependency, t => Activator.CreateInstance(t, parameterImplementation));
            }
            return CreateImplementaiton(dependency, t => Activator.CreateInstance(type));
        }
        public object CreateImplementaiton(Dependency dependency, Func<Type, object> factory)
        {
            if (dependency.Implemented)
                return dependency.Implementation;
            var implementation = factory(dependency.Type);
            if (dependency.Lifetime == DependencyLifetime.Singleton)
            {
                dependency.AddImplementation(implementation);
            }
            return implementation;
        }
    }
    public class Dependency
    {
        public Dependency(Type t, DependencyLifetime l)
        {
            Type = t;
            Lifetime = l;
        }

        public Type Type { get; set; }
        public DependencyLifetime Lifetime { get; set; }
        public object Implementation { get; set; }
        public bool Implemented { get; set; }
        public void AddImplementation(object i)
        {
            Implementation = i;
            Implemented = true;
        }
    }
    public enum DependencyLifetime
    {
        Singleton = 0,
        Transient = 1,
    }
    public class DependencyContainer
    {
        List<Dependency> _dependencies;
        public DependencyContainer()
        {
            _dependencies = new List<Dependency>();
        }
        public void AddSingleton<T>()
        {
            _dependencies.Add(new Dependency(typeof(T), DependencyLifetime.Singleton));
        }
        public void AddTransient<T>()
        {
            _dependencies.Add(new Dependency(typeof(T), DependencyLifetime.Transient));
        }

        public Dependency GetDependency(Type type)
        {
            return _dependencies.First(x => x.Type.Name == type.Name);
        }
    }
    public class ServiceConsumer
    {
        HelloService _helloService;
        public ServiceConsumer(HelloService helloService)
        {
            _helloService = helloService;
        }
        public void Print()
        {
            _helloService.Print();
        }
    }
    public class HelloService
    {
         MessageService _message;
        int _random;
        public HelloService(MessageService message)
        {
            _message = message;
            _random = new Random().Next();
        }
        public void Print()
        {
            Console.WriteLine($"Hello {_random} World{_message.Message()}");
        }
    }
    public class MessageService
        {
        private int _random;
        public MessageService()
        {
            _random = new Random().Next();
        }
        public string Message()
        {
            return $"Yo # {_random}";
        }
}
}
