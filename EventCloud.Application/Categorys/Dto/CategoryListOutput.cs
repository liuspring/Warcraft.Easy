using System;
using Abp.AutoMapper;

namespace EventCloud.Categorys.Dto
{
    [AutoMapFrom(typeof(Category))]
    [Serializable]
    public class CategoryListOutput
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        [NonSerialized] 
        public DateTime CreationTime;

        public string SCreationTime
        {
            get { return CreationTime.ToString("yy-MM-dd HH:mm:ss"); }
        }

    }
}
