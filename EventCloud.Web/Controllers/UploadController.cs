using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Web.Controllers
{
    public class UploadController : TaskManagerControllerBase
    {

        private readonly string uploadFilePath = ConfigurationManager.AppSettings["UploadFilePath"];



        /// <summary>
        /// 上传文件返回上次结果
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadFile()
        {
            var fileName = string.Empty;
            var filePath = string.Empty;
            try
            {
                var uploadObj = Request.Files[0];
                if (uploadObj != null && uploadObj.ContentLength > 0)
                {
                    fileName = uploadObj.FileName;
                    var et = Path.GetExtension(fileName);
                    var currentFileName = DateTime.Now.ToString("yyyyMMddHHmmss") +
                                   Guid.NewGuid().ToString().Substring(0, 5) + et;
                    var uploadFile = Server.MapPath(uploadFilePath);
                    if (!Directory.Exists(uploadFile))
                    {
                        Directory.CreateDirectory(uploadFile);
                    }
                    filePath = uploadFile +"//"+ currentFileName;
                    uploadObj.SaveAs(filePath);
                    filePath = uploadFilePath +"//"+ currentFileName;
                }
            }
            catch (Exception )
            {
                fileName = string.Empty;
            }
            if (string.IsNullOrEmpty(fileName))
                return Json(new { ret = false, msg = "上传文件失败！" });
            return Json(new { ret = true, filename = fileName, filesavename = filePath });
        }
    }
}