using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Test t = new Test();
            // 颠倒给定的 32 位无符号整数的二进制位
            //long n = t.reverseBits(5541);

            //搜索二维数组
            //int[][] matrix = new int[][] { new int[] { 1, 3, 5, 7 }, new int[] { 10, 11, 16, 20 }, new int[] { 23, 30, 34, 60 } };
            //int target = 3;
            //bool res =t.SearchMatrix(matrix, target);
            //Console.WriteLine(res);

            //返回该数组所有可能的子集
            List<List<int>> res_list = t.SubsetsWithDup(new int[]{ 1,2,2});
            string res = "[";
            foreach(var p in res_list)
            {
                string res_p = "[";
                foreach(var n in p)
                {
                    res_p += n;
                    res_p += ",";
                }
                res_p += "],";
                res += res_p;
            }
            res += "]";
            Console.WriteLine(res);


            Console.ReadLine();
        }

    }
    class Test
    {
        /// <summary>
        /// 颠倒给定的 32 位无符号整数的二进制位
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public uint reverseBits(uint n)
        {
            uint res_Num = 0;
            Console.WriteLine("Input Num:" + n);
            //转换成二进制字符串
            string str_Num = Convert.ToString(n, 2);
            #region 方法一:先将数字转换成32位字符串，然后将字符串倒转
            //str_Num = str_Num.PadLeft(32, '0');
            //Console.WriteLine("Convert To Binary string:" + str_Num);
            ////将字符串颠倒
            //List<char> list = str_Num.ToList();
            //string str_New = "";
            //for (int i = str_Num.Length - 1; i >= 0; i--)
            //{
            //    str_New += list[i];
            //}
            //Console.WriteLine("Reverse the Binary string:" + str_New);
            ////转换成int类型
            //res_Num = Convert.ToUInt32(str_New, 2);

            #endregion
            #region 方法二：直接将int数据逐位颠倒          
            for (int i = 0; i < 32; i++)
            {
                res_Num |= (n & 1) << (31 - i);
                n >>= 1;
            }
            #endregion
            Console.WriteLine("Convert to uint:" + res_Num);
            return res_Num;
        }

        /// <summary>
        /// 搜索二维数组
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool SearchMatrix(int[][] matrix, int target)
        {
            #region 方法一：直接遍历
            //foreach(var arr in matrix)
            //{
            //    if (arr.First(p => p == target) == target)
            //    {
            //        Console.WriteLine("find");
            //        return true;
            //    }
            //}
            //foreach (var arr in matrix)
            //{
            //    foreach (int i in arr)
            //    {
            //        if (i == target) return true;
            //    }
            //}

            //foreach (var arr in matrix)
            //{
            //    if (arr[0] == target || arr[arr.Length - 1] == target) return true;
            //    else if (arr[0] < target && arr[arr.Length - 1] > target)
            //    {
            //        for (int i = 1; i < arr.Length - 1; i++)
            //            if (arr[i]== target) return true;

            //    }
            //}
            #endregion

            #region 方法二：使用二分法查找
            int low = 0;
            int high = matrix.Length * matrix[0].Length - 1;
            //行号
            int row = matrix.Length;
            //列号
            int col = matrix[0].Length;
            while (low <= high)
            {
                int mid = (high - low) / 2 + low;
                int x = matrix[mid / col][mid % col];
                if (x < target)
                    low = mid + 1;
                else if (x > target)
                    high = mid - 1;
                else
                    return true;
            }

            #endregion
            return false;
        }

        /// <summary>
        /// 返回该数组所有可能的子集（幂集）
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public List<List<int>> SubsetsWithDup(int[] nums)
        {
            List<List<int>> list = new List<List<int>>();
            int[] signal = new int[nums.Length];
            list.Add(new List<int>());
            list.Add(nums.ToList());
            for(int i = 0; i < nums.Length ; i++)
            {
                for(int j=i;j<nums.Length-1;j++)
                {
                    List<int> l = new List<int>();
                    l.Add(nums[j]);
                    if (!Distinct(list,l))
                    {
                        list.Add(l);
                    }
                    
                }
            }
            return list;
        }

        /// <summary>
        /// 判断去重
        /// </summary>
        /// <param name="list"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        private bool Distinct(List<List<int>> list, List<int> l)
        {
            foreach(var t in list)
            {
                if (t.Count == l.Count)
                {
                    int[] res = new int[l.Count];
                    for(int i=0;i<l.Count;i++)
                    {
                        if (t.Contains(l[i]))
                            res[i] = 1;
                    }
                    int count = 0;
                    foreach(var r in res)
                    {
                        if (r == 1) count++;
                    }
                    if (count == res.Length) return true;
                }
                else continue;
            }
            return false;
        }


    }
}
