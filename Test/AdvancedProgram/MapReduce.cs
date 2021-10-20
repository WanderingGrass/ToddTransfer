using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgram
{
    /// <summary>
    /// 利用分而治之。
    /// 淘宝web服务器上有1个access日志文件，记录着用户访问的url，url总数100亿以上，每个url约占64字节，这些url可能存在重复，在一个内存只有2G的机器上，统计出访问频率最高的前100个URL。
    /// 考察点1：MapReduce思想，利用中间文件存储，分而治之。考察点2：排序算法解题思路：100亿*64/1024/1024/1024 = 596G, 可考虑分成1000个文件处理，每个文件大约600M。 顺序读取文件，每行按照hash(url)%1000的结果将url写入到1000个文件中，这个过程是mapreduce中的map。针对每个小文件，使用hashmap统计每个url出现的次数，并使用堆排序得到访问次数最高的前100个url，将每个文件排序好的100个url及对应的count输出到1000个文件，最后将这个1000个文件（此时每个文件只有100行)进行合并排序。
    /// </summary>
    public class MapReduce
    {
        /// <summary>
        /// 划分
        /// </summary>
        public void Divide()
        {
            
        }
        public void ReadDivide()
        {
            
        }
        public void Sort()
        {
            
        }
    }
}
