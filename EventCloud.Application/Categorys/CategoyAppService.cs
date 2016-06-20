﻿using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using TaskManager.Categorys.Dto;

namespace TaskManager.Categorys
{
    public class CategoyAppService : TaskManagerAppServiceBase, ICategoyAppService
    {
        private readonly IRepository<Category, int> _categoryRepository;

        public CategoyAppService(IRepository<Category, int> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public int Create(CreateCategoryInput input)
        {
            var category = Category.Create(input.CategoryName);
            _categoryRepository.Insert(category);
            return category.Id;
        }

        public List<CategoryListOutput> GetList(CategoryListInput input)
        {
            var categorys = _categoryRepository
                .GetAllList()
                .OrderBy(a => a.Id)
                .Skip(input.iDisplayStart)
                .Take(input.iDisplayLength);
            return categorys.MapTo<List<CategoryListOutput>>();
        }

        public int GetListTotal(CategoryListInput input)
        {
            return _categoryRepository.GetAllList().Count;
        }

        public List<Category> GetAllList()
        {
            return _categoryRepository.GetAllList();
        }
    }
}
