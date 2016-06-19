using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventCloud.Categorys;
using EventCloud.Nodes;
using EventCloud.Tasks;
using EventCloud.Tasks.Dto;

namespace EventCloud.Web.Controllers
{
    public class TaskController : EventCloudControllerBase
    {
        private readonly ITaskAppService _taskAppService;
        private readonly ICategoyAppService _categoyAppService;
        private readonly INodeAppService _nodeAppService;

        public TaskController(ITaskAppService taskAppService, ICategoyAppService categoyAppService, INodeAppService nodeAppService)
        {
            _taskAppService = taskAppService;
            _categoyAppService = categoyAppService;
            _nodeAppService = nodeAppService;
        }

        //
        // GET: /Task/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获得任务列表
        /// </summary>
        /// <returns></returns>
        public JsonResult AjaxTaskList()
        {
            var input = new TaskListInput(Request);
            var count = _taskAppService.GetListTotal(input);
            var result = count == 0 ? new List<TaskListOutput>() : _taskAppService.GetList(input);
            var response = new DataTablesResponse
            {
                recordsTotal = count,
                data = result
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Task/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Task/Create
        public ActionResult Create()
        {
            var categorys = _categoyAppService.GetAllList();
            ViewData["Categories"] = new SelectList(categorys, "Id", "CategoryName");
            var nodes = _nodeAppService.GetAllList();
            ViewData["Nodes"] = new SelectList(nodes, "Id", "NodeName");
            return View();
        }

        //
        // POST: /Task/Create
        [HttpPost]
        public ActionResult AjaxCreate(CreateTaskInput input)
        {
            var res = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                _taskAppService.Create(input);
                res.Data = new { ret = true };
            }
            catch (Exception ex)
            {
                res.Data = new { ret = false, msg = "保存失败：" + ex.Message };
            }
            return res;
        }

        //
        // GET: /Task/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Task/Edit/5
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
        // GET: /Task/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Task/Delete/5
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
