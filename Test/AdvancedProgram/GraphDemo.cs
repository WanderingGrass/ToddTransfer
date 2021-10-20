using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgram
{
    public class GraphDemo
    {
        static List<T> MySort<T>(IEnumerable<T> source, Func<T, IEnumerable<T>> getDepends)
        {
            // 访问过的路径
            Dictionary<T, bool> visited = new Dictionary<T, bool>();
            // 已经排过序的
            List<T> sorted = new List<T>();
            foreach (var item in source)
            {
                Visit<T>(item, getDepends, visited, sorted);
            }
            return sorted;
        }

        static void Visit<T>(T item, Func<T, IEnumerable<T>> getDepends, Dictionary<T, bool> visited, List<T> sorted)
        {
            //已经访问过了
            if (visited.ContainsKey(item))
            {
                bool isVisit = visited[item];
                if (isVisit == true)
                {
                    throw new Exception("循环引用");
                }
            }
            //未访问
            else
            {
                visited.Add(item, true);//true :正在访问 false:访问完成

                //获取所有依赖
                var depends = getDepends(item);
                foreach (var depend in depends)
                {
                    Visit(depend, getDepends, visited, sorted);
                }

                //访问完成
                visited[item] = false;
                sorted.Add(item);
            }

        }
    }
}
