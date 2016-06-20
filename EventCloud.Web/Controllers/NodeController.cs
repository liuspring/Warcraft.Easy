using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TaskManager.Nodes;
using TaskManager.Nodes.Dto;

namespace TaskManager.Web.Controllers
{
    public class NodeController : TaskManagerControllerBase
    {
        private readonly INodeAppService _nodeAppService;
        public NodeController(INodeAppService nodeAppService)
        {
            _nodeAppService = nodeAppService;
        }

        //
        // GET: /Node/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获得节点列表
        /// </summary>
        /// <returns></returns>
        public JsonResult AjaxNodeList()
        {
            var input = new NodeListInput(Request);
            var count = _nodeAppService.GetListTotal(input);
            var result = count == 0 ? new List<NodeListOutput>() : _nodeAppService.GetList(input);
            var response = new DataTablesResponse
            {
                recordsTotal = count,
                data = result
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Node/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Node/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Node/Create
        [HttpPost]
        public ActionResult AjaxCreate(CreateNodeInput input)
        {
            var res = new JsonResult();
            try
            {
                // TODO: Add insert logic here
                _nodeAppService.Create(input);
                res.Data = new { ret = true };
            }
            catch (Exception ex)
            {
                res.Data = new { ret = false, msg = "保存失败：" + ex.Message };
            }
            return res;
        }

        //
        // GET: /Node/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Node/Edit/5
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
        // GET: /Node/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Node/Delete/5
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
