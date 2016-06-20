using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Commands;
using TaskManager.Commands.Dto;
using TaskManager.Nodes;
using TaskManager.Tasks;

namespace TaskManager.Web.Controllers
{
    public class CommandController : TaskManagerControllerBase
    {
        private readonly ICommandAppService _commandAppService;
        private readonly ITaskAppService _taskAppService;
        private readonly INodeAppService _nodeAppService;

        public CommandController(ICommandAppService commandAppService, ITaskAppService taskAppService, NodeAppService nodeAppService)
        {
            _commandAppService = commandAppService;
            _taskAppService = taskAppService;
            _nodeAppService = nodeAppService;
        }
        //
        // GET: /Command/
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获得分类列表
        /// </summary>
        /// <returns></returns>
        public JsonResult AjaxCommandList()
        {
            var input = new CommandListInput(Request);
            var count = _commandAppService.GetListTotal(input);
            var result = count == 0 ? new List<CommandListOutput>() : _commandAppService.GetList(input);
            var response = new DataTablesResponse
            {
                recordsTotal = count,
                data = result
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Command/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Command/Create
        public ActionResult Create()
        {
            var nodes = _nodeAppService.GetAllList();
            ViewData["Nodes"] = new SelectList(nodes, "Id", "NodeName");
            var tasks = _taskAppService.GetAllList();
            ViewData["Tasks"] = new SelectList(tasks, "Id", "TaskName");
            return View();
        }

        //
        // POST: /Command/Create
        [HttpPost]
        public ActionResult AjaxCreate(CreateCommandInput input)
        {
            var res = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                _commandAppService.Create(input);
                res.Data = new { ret = true };
            }
            catch (Exception ex)
            {
                res.Data = new { ret = false, msg = "保存失败：" + ex.Message };
            }
            return res;
        }

        //
        // GET: /Command/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Command/Edit/5
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
        // GET: /Command/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Command/Delete/5
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
