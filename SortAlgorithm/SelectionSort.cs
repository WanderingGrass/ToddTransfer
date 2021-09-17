using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithm
{
    /// <summary>
    /// 选择排序
    /// O（n²）-O(1)-非稳定排序（如a在b前，排序后可能出现在b后面）
    /// 要点：规模越小越好，好处就是不占用额外的内存空间
    /// </summary>
    public class SelectionSort
    {
        public void Sort(int[] arr)
        {
            int length = arr.Length;
            int minIndex, temp;
            for (int i = 0; i < length-1; i++)
            {
                minIndex = i;
                for (int j = i+1; j < length; j++)
                {
                    if (arr[j] < arr[minIndex]) // 寻找最小的数
                    {
                        minIndex = j;// 将最小数的索引保存
                    }
                }
                temp = arr[i];
                arr[i] = arr[minIndex];
                arr[minIndex] = temp;
            }
        }
    }
}
