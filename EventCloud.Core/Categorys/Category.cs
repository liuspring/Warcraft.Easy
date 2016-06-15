using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;

namespace EventCloud.Categorys
{
    [Table("qrtz_category")]
    public class Category : FullAuditedEntity
    {

        [Column("id")]
        public override int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("category_name")]
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
