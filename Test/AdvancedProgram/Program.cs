using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AdvancedProgram
{
    public class Program
    {
        public static async  Task Main(string[] args) {
            YieldDemo yieldDemo = new YieldDemo();
            yieldDemo.GetGenerators();
        }
    }
}
