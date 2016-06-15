﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EventCloud.Categorys.Dto;

namespace EventCloud.Categorys
{
    public interface ICategoyAppService
    {
        int Create(CreateCateoryInput input);

        List<CategoryListOutput> GetList(CategoryListInput input);
        int GetListTotal(CategoryListInput input);
    }
}
