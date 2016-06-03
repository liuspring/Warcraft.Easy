using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common {
    public class MyConvert {
        /// <summary>
        /// 用2的幂的和表示一个整形数组
        /// </summary>
        public static int BitsToInt32(List<Int16> v) {
            int sum = 0;
            if (v != null && v.Count > 0) {
                foreach (int i in v) {
                    if (i >= 0) {
                        sum |= 1 << i;
                    }
                }
            }
            return sum;
        }
        /// <summary>
        /// 用2的幂的和取得一个int数组
        /// </summary>
        public static List<Int16> Int32ToBits(int v) {
            List<Int16> ret = null;
            ret = new List<Int16>();
            for (var i = 0; i < 32; ++i) {
                if ((v & (1 << i)) != 0) {
                    ret.Add((Int16)i);
                }
            }
            return ret;
        }
        /// <summary>
        /// 取的一个2的幂的整数是不是包含某个整数
        /// </summary>
        /// <param name="bits">用2的幂的和生成的整数</param>
        /// <param name="index">某个检查的整数</param>
        /// <returns>
        /// true 包含
        /// false 不包含
        /// </returns>
        public static bool GetBit(int bits, Int16 index) {
            return (bits & (1 << index)) != 0;
        }
        /// <summary>
        /// 设置一个2的幂的整数包含某个整数
        /// </summary>
        /// <param name="bits">用2的幂的和生成的整数</param>
        /// <param name="index">某个检查的整数</param>
        public static void SetBit(ref int bits, Int16 index) {
            bits |= (1 << index);
        }
        /// <summary>
        /// 重置一个2的幂的整数不包含某个整数
        /// </summary>
        /// <param name="bits">用2的幂的和生成的整数</param>
        /// <param name="index">某个检查的整数</param>
        public static void ResetBit(ref int bits, Int16 index) {
            bits &= ~(1 << index);
        }
        /// <summary>
        /// 用2的幂的和表示一个整形数组
        /// </summary>
        public static Int64 BitsToInt64(List<Int32> v) {
            Int64 sum = 0;
            if (v != null && v.Count > 0) {
                foreach (int i in v) {
                    if (i >= 0) {
                        sum |= (Int64)(1) << i;
                    }
                }
            }
            return sum;
        }
        /// <summary>
        /// 用2的幂的和取得一个int数组
        /// </summary>
        public static List<Int32> Int64ToBits(Int64 v) {
            List<Int32> ret = null;
            ret = new List<Int32>();
            for (var i = 0; i < 64; ++i) {
                if ((v & ((Int64)1 << i)) != 0) {
                    ret.Add(i);
                }
            }
            return ret;
        }
        /// <summary>
        /// 取的一个2的幂的整数是不是包含某个整数
        /// </summary>
        /// <param name="bits">用2的幂的和生成的整数</param>
        /// <param name="index">某个检查的整数</param>
        public static bool GetBit(Int64 bits, int index) {
            return (bits & (1 << index)) != 0;
        }
        /// <summary>
        /// 设置一个2的幂的整数包含某个整数
        /// </summary>
        /// <param name="bits">用2的幂的和生成的整数</param>
        /// <param name="index">某个检查的整数</param>
        public static void SetBit(ref Int64 bits, int index) {
            bits |= Convert.ToInt64(1 << index);
        }
        /// <summary>
        /// 重置一个2的幂的整数不包含某个整数
        /// </summary>
        public static void ResetBit(ref Int64 bits, int index) {
            bits &= ~(1 << index);
        }
        public static byte[] IntArryToBytes(int[] intArry) {
            int length = intArry.Length;
            byte[] byteArry = new byte[length * 4];
            int index = 0;
            foreach (int n in intArry) {
                byteArry[index++] = (byte)(n & 0xff);
                byteArry[index++] = (byte)((n >> 8) & 0xff);
                byteArry[index++] = (byte)((n >> 16) & 0xff);
                byteArry[index++] = (byte)((n >> 24) & 0xff);
            }
            return byteArry;
        }
        public static int[] BytesToIntArry(byte[] byteArry) {
            int[] intArry = new int[byteArry.Length / 4];
            int index = 0;
            for (int m = 0; m < byteArry.Length; m += 4) {
                intArry[index++] = (int)byteArry[m] | (int)(byteArry[m + 1] << 8) | (int)(byteArry[m + 2] << 16) | (int)(byteArry[m + 3] << 24);
            }
            return intArry;
        }
    }
}
