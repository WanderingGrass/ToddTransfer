using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithm
{
    /// <summary>
    /// 计数排序
    /// </summary>
  public  class CountingSort
    {
        public void Sort(int[] arr,int arrange)
        {
            //array to store the sorted result, 
            //size is the same with input array. 
            int[] arrayResult = new int[arr.Length];
            //array to store the direct value in sortingprocess 
            //include index 0;     
            //size is arrange+1;     
            int[] arrayTemp = new int[arrange + 1];
            //clear up the temp array     
            for (int i = 0; i <= arrange; i++)
            {
                arrayTemp[i] = 0;
            }
            //now temp array stores the count of valueequal 
            for (int j = 0; j < arr.Length; j++)
            {
                arrayTemp[arr[j]] += 1;
            }
            //now temp array stores the count of value lower andequal 
            for (int k = 1; k <= arrange; k++)
            {
                arrayTemp[k] += arrayTemp[k - 1];
            }
            //output the value to result     
            for (int m = arr.Length - 1; m >= 0; m--)
            {
                arrayResult[arrayTemp[arr[m]] - 1] = arr[m];
                arrayTemp[arr[m]] -= 1;
            }
        }
    }
}
