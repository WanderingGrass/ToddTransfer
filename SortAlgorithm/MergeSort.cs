using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithm
{
    /// <summary>
    /// 归并排序
    /// O(nlog2n)-O(n)-稳定排序（如a在b前，排序后a还在b前）
    /// </summary>
    public class MergeSort
    {
        public void Sort(int[] arr)
        {
            int length = arr.Length;
            if (length < 2)
            {
                return;
            }
            Sort(arr, 0, length - 1);
        }
        private void Sort(int[] arr,int left, int right)
        {
            //数组一分为2，不断拆分，直到数组长度为1，直接返回
            if (left >= right)
            {
                return;
            }
            int mid = left + (right - left) / 2;
            Sort(arr, left, mid);
            Sort(arr, mid + 1, right);

            //按mid将数组分组，然后将两组数据归并为1组
            int[] arr2 = new int[right - left + 1];
            int index = 0;
            int i = left;
            int j = mid + 1;

            while (i <= mid && j <= right)
            {
                if (arr[i] <= arr[j])
                {
                    arr2[index] = arr[i];
                    i++;
                    index++;
                }
                else
                {
                    arr2[index] = arr[j];
                    j++;
                    index++;

                }
            }

            while (i <= mid)
            {
                arr2[index] = arr[i];
                i++;
                index++;
            }

            while (j <= right)
            {
                arr2[index] = arr[j];
                j++;
                index++;
            }


            for (int n = 0; n < index; n++)
            {
                arr[left + n] = arr2[n];
            }
        }
    }
}
