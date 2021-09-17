using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithm
{
    /// <summary>
    /// 插入排序
    /// O（n²）-O(1)-稳定排序（如a在b前，排序后a还在b前）
    /// </summary>
    public class InsertionSort
    {
        public void Sort(int[] arr)
        {
            int len = arr.Length;
            int preIndex, current;
            for (int i = 1; i < len; i++)
            {
                preIndex = i - 1;
                current = arr[i];
                while (preIndex>0&&arr[preIndex]>current)
                {
                    arr[preIndex + 1] = arr[preIndex];
                    preIndex--;
                }
                arr[preIndex + 1] = current;
            }
        }
    }
}
