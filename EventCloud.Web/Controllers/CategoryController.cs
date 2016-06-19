using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCloud.Categorys;
using EventCloud.Categorys.Dto;

namespace EventCloud.Web.Controllers
{
    public class CategoryController : EventCloudControllerBase
    {

        private readonly ICategoyAppService _categoyAppService;

        public CategoryController(ICategoyAppService categoyAppService)
        {
            _categoyAppService = categoyAppService;
        }

        //
        // GET: /Category/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获得分类列表
        /// </summary>
        /// <returns></returns>
        public JsonResult AjaxCategoryList()
        {
            var input = new CategoryListInput(Request);
            var count = _categoyAppService.GetListTotal(input);
            var result = count == 0 ? new List<CategoryListOutput>() : _categoyAppService.GetList(input);
            var response = new DataTablesResponse
            {
                recordsTotal = count,
                data = result
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        //
        // GET: /Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Category/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Category/Create
        [HttpPost]
        public JsonResult AjaxCreate(CreateCategoryInput input)
        {
            var res = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                _categoyAppService.Create(input);
                res.Data = new { ret = true };
            }
            catch (Exception ex)
            {
                res.Data = new { ret = false, msg = "保存失败：" + ex.Message };
            }
            return res;
        }

        //
        // GET: /Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
