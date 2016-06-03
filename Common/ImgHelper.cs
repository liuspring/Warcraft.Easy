using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public static class ImgHelper
    {
        public static string GenerateImgHtml(this string img)
        {
            var sb = new StringBuilder();
            var list = new List<string>();
            if (!string.IsNullOrEmpty(img))
            {
                list = ObjHelper.Deserialize<List<string>>(img);
            }
            if (list == null || list.Count <= 0) return sb.ToString();
            for (int i = 0 , j = list.Count; i < j; i++)
            {
                sb.Append("<a target=\"_blank\" style=\"margin-right:5px\" href=" + list[i].SafeImgPath() + ">[" + (i + 1) + ".jpg" + "]</a>");
            }
            return sb.ToString();
        }

        public static string GenerateImgHtmlForObject(this string img)
        {
            var sb = new StringBuilder();
            var list = new List<ExtraChangeImg>();
            if (!string.IsNullOrEmpty(img))
            {
                list = ObjHelper.Deserialize<List<ExtraChangeImg>>(img);
            }
            if (list == null || list.Count <= 0) return sb.ToString();
            for (int i = 0, j = list.Count; i < j; i++)
            {
                sb.Append("<a target=\"_blank\" style=\"margin-right:5px\" href=" + list[i].image.SafeImgPath() + ">[" + (i + 1) + ".jpg" + "]</a>");
            }
            return sb.ToString();
        }

        private class ExtraChangeImg
        {
            public string image { get; set; }

            public string thumbnail { get; set; }
        }
    }
}
