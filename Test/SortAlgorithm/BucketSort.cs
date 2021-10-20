using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorithm
{
   public class BucketSort
    {
        private int[] _arr;

        public BucketSort(int[] arr)
        {
            _arr = arr;
        }
        public void Sort()
        { 
            /*
         *  第一步：将数组堆化
         *  beginIndex = 第一个非叶子节点。
         *  从第一个非叶子节点开始即可。无需从最后一个叶子节点开始。
         *  叶子节点可以看作已符合堆要求的节点，根节点就是它自己且自己以下值为最大。
         */
            int len = _arr.Length-1;
            int beginIndex = (len - 1) >> 1;
            for (int i = beginIndex; i >=0; i--)
            {
                MaxHeapify(i, len);
            }
            /*
        * 第二步：对堆化数据排序
        * 每次都是移出最顶层的根节点A[0]，与最尾部节点位置调换，同时遍历长度 - 1。
        * 然后从新整理被换到根节点的末尾元素，使其符合堆的特性。
        * 直至未排序的堆长度为 0。
        */
            for (int i = len; i > 0; i--)
            {
                swap(0, i);
                MaxHeapify(0, i - 1);
            }
        }
        private void swap(int i, int j)
        {
            int temp = _arr[i];
            _arr[i] = _arr[j];
            _arr[j] = temp;
        }
        /**
    * 调整索引为 index 处的数据，使其符合堆的特性。
    *
    * 需要堆化处理的数据的索引
    *  未排序的堆（数组）的长度
    */
        private void MaxHeapify(int index, int len)
        {
            int li = (index << 1) + 1;// 左子节点索引
            int ri = li + 1;// 右子节点索引
            int cMax = li;// 子节点值最大索引，默认左子节点。
            if (li>len)return;// 左子节点索引超出计算范围，直接返回。
            if (ri <= len && _arr[ri] > _arr[li])// 先判断左右子节点，哪个较大。
                cMax = ri;
            if (_arr[cMax] > _arr[index])
            {
                swap(cMax, index);// 如果父节点被子节点调换，
                MaxHeapify(cMax, len);// 则需要继续判断换下后的父节点是否符合堆的特性。
            }

        }
    }
}
