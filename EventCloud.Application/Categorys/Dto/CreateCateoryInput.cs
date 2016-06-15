using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace EventCloud.Categorys.Dto
{
    [AutoMapFrom(typeof(Category))]
    public class CreateCateoryInput : IInputDto
    {
        public const int MaxCategoryNameLength = 50; 

        [Required]
        [StringLength(MaxCategoryNameLength)]
        public string CategoryName { get; set; }

    }
}
