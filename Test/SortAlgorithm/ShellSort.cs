using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithm
{
    /// <summary>
    /// 希尔排序
    /// O(n²)-O（1）-非稳定排序（如a在b前，排序后可能出现在b后面）
    /// </summary>
    public class ShellSort
    {
        public void Sort(int[] arr)
        {
            int len = arr.Length,gap = 1;
            while (gap< len / 3)
            {
                gap = gap * 3 + 1;
            }
            while (gap>0)
            {
                for (int i = gap; i < len; i++)
                {
                    int temp = arr[i];
                    int j = i - gap;
                    while (j>=0&&arr[j]>temp)
                    {
                        arr[j + gap] = arr[j];
                        j -= gap;
                    }
                    arr[j + gap] = temp;
                }
                decimal result = gap / 3;
                gap = (int)Math.Floor(result); ;
            }
        }
    }
}
