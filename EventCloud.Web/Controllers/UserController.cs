using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.AutoMapper;
using EventCloud.Users;
using EventCloud.Users.Dto;

namespace EventCloud.Web.Controllers
{
    public class UserController : EventCloudControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly UserManager _userManager;
        public UserController(UserManager userManager, IUserAppService userAppService)
        {
            _userManager = userManager;
            _userAppService = userAppService;
        }

        #region Account Manager
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult UserList()
        {
            return View();
        }

        /// <summary>
        /// 获取用户列表信息
        /// </summary>
        /// <returns></returns>
        public JsonResult AjaxUserList()
        {
            DataTablesResponse response;
            var input = new UserListInput(Request);
            var tCount = _userAppService.GetListTotal(input);
            if (tCount.Result == 0)
            {
                response = new DataTablesResponse();
            }
            else
            {
                var tResult = _userAppService.GetList(input);
                response = new DataTablesResponse()
                {
                    recordsTotal = tCount.Result,
                    data = tResult.Result.Items
                };
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用户编辑页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CreateUser(long id = 0)
        {
            if (id == 0)
                return View(new CreateUserInput());
            var tUser = _userManager.GetUserByIdAsync(id);
            if (tUser.IsFaulted)
                return View(new CreateUserInput());
            var model = tUser.Result.MapTo<CreateUserInput>();
            return View(model);
        }

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult AjaxSaveUser(CreateUserInput model)
        {
            var res = new JsonResult();
            try
            {
                CheckModelState();
                _userAppService.Save(model);
                res.Data = new { ret = true };
            }
            catch (Exception ex)
            {
                res.Data = new { ret = false, msg = "保存失败：" + ex.Message };
            }
            return res;
        }

        #endregion
    }
}