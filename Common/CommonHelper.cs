
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class CommonHelper
    {
        /// <summary>
        /// MD5摘要
        /// </summary>
        /// <param name="argInput">输入字符串</param>
        /// <param name="isUppder">返回是否大写</param>
        /// <returns>MD5哈希值</returns>
        public static string GetMd5Hash(string argInput, bool isUppder = false)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(argInput));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString(isUppder ? "X2" : "x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 对比两个字符串是否相等（不分大小写）
        /// </summary>
        /// <param name="stringX">字符串1</param>
        /// <param name="stringY">字符串2</param>
        /// <returns>true：相等  false：不相等</returns>
        public static bool Comparer2String(string stringX, string stringY)
        {
            StringComparer stringComparer = StringComparer.InvariantCultureIgnoreCase;
            return stringComparer.Compare(stringX, stringY) == 0;
        }

        /// <summary>
        /// 产生随机字符串
        /// </summary>
        /// <param name="num">随机出几个字符</param>
        /// <param name="randomType">随机数类型（0：数字；1：字母和数字；2：汉字）</param>
        /// <returns>随机出的字符串</returns>
        public static string GenRandomCode(int num, int randomType = 0)
        {
            string str;
            switch (randomType)
            {
                case 0:
                    str = "123456789";
                    break;
                case 1:
                    str = "123456789ABCDEFGHIJKLMNPQRSTUVWXYZabcdefghijklmnpqrstuvwxyz";
                    break;
                case 2:
                    //常用498个汉字
                    str = "的一是在不了有和人这中大为上个国我以要他时来用们生到作地于出就分对成会可主发年动同工也能下过子说产种面而方后多定行学法所民得经十三之进着等部度家电力里如水化高自二理起小物现实加量都两体制机当使点从业本去把性好应开它合还因由其些然前外天政四日那社义事平形相全表间样与关各重新线内数正心反你明看原又么利比或但质气第向道命此变条只没结解问意建月公无系军很情者最立代想已通并提直题党程展五果料象员革位入常文总次品式活设及管特件长求老头基资边流路级少图山统接知较将组见计别她手角期根论运农指几九区强放决西被干做必战先回则任取据处队南给色光门即保治北造百规热领七海口东导器压志世金增争济阶油思术极交受联什认六共权收证改清己美再采转更单风切打白教速花带安场身车例真务具万每目至达走积示议声报斗完类八离华名确才科张信马节话米整空元况今集温传土许步群广石记需段研界拉林律叫且究观越织装影算低持音众书布复容儿须际商非验连断深难近矿千周委素技备半办青省列习响约支般史感劳便团往酸历市克何除消构府称太准精值号率族维划选标写存候毛亲快效斯院查江型眼王按格养易置派层片始却专状育厂京识适属圆包火住调满县局照参红细引听该铁价严";
                    break;
                default:
                    str = "123456789";
                    break;
            }

            string code = "";
            var rd = new Random();
            int i;
            for (i = 0; i < num; i++)
            {
                code += str.Substring(rd.Next(0, str.Length), 1);
            }
            return code;
        }
        /// <summary>
        /// 产生一个唯一标识字符串
        /// </summary>
        /// <returns></returns>
        public static string GenerationUuid()
        {
            return Guid.NewGuid().ToString().Replace("-","");
        }

        /// <summary>
        /// 是否是正确的经度
        /// </summary>
        /// <returns></returns>
        public static bool IsLon(double inputLon)
        {
            return inputLon > 0 && inputLon < 180;
        }

        /// <summary>
        /// 是否为正确的维度
        /// </summary>
        /// <param name="inputLat"></param>
        /// <returns></returns>
        public static bool IsLat(double inputLat)
        {
            return inputLat > 0 && inputLat < 90;
        }

        public static string FormatCategoryList(List<string> list)
        {
            if (list.Count > 0)
            {
                string str = list[0];
                list.RemoveAt(0);
                return str + "(" + string.Join("/", list) + ")";
            }
            return "";
        }
    }
}
