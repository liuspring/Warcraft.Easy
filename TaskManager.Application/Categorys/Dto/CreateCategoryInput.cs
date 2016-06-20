using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace TaskManager.Categorys.Dto
{
    [AutoMapFrom(typeof(Category))]
    public class CreateCategoryInput : IInputDto
    {
        public const int MaxCategoryNameLength = 50; 

        [Required]
        [StringLength(MaxCategoryNameLength)]
        public string CategoryName { get; set; }

    }
}
