using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Categorys
{
    [Table("qrtz_category")]
    [Description("任务分类表")]
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [Column("category_name")]
        [Description("分类名称")]
        public string CategoryName { get; set; }

        protected Category()
        {
        }

        public static Category Create(string categoryName)
        {
            var category = new Category
            {
                CategoryName = categoryName
            };
            return category;
        }
    }
}
