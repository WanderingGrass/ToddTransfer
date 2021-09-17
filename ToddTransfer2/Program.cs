using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;
using System.Buffers;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace ToddTransfer2
{
   public class Program
    {
        public static void Main()
        {
            string s = "测试";
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(s);
            int w = byteArray.Length;
            int j = s.Length;
            Console.WriteLine(w +","+j);
        }
    }
}
