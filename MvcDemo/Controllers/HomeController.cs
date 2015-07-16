using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
        public ActionResult About(int i = 1)
        {
            return View();
        }


        public ActionResult UEditor()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UEditor(int i = 1)
        {
            string content = Request.Form.Get("editorValue");

            ViewData["content"] = content;
            return View();
        }



        public ActionResult Plupload()
        {
            return View();
        }


        [HttpPost]
        public virtual JsonResult Upload()
        {
            DateTime NowTime = DateTime.Now;

            string savePath = string.Format("/Yuonhtt_FileUpload/img/{0}/{1}/{2}/", NowTime.Year, NowTime.Month.ToString("D2"), NowTime.Day.ToString("D2"));//上传文件的路径 

            var localPath = Server.MapPath(savePath);
            if (!Directory.Exists(Path.GetDirectoryName(localPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(localPath));
            }
            HttpRequest request = System.Web.HttpContext.Current.Request;
            string src = string.Empty;

            if (request.Files.Count > 0)
            {
                for (int i = 0; i < request.Files.Count; i++)
                {
                    HttpPostedFile file = request.Files[i];
                    string extension = Path.GetExtension(file.FileName);
                    string fileName = string.Format("{0}{1}", DateTime.Now.ToString("HHmmss") + new Random().Next(100000, 999999), extension);
                    file.SaveAs(localPath + fileName);
                    src = savePath + fileName;
                }
            }
            return Json(new { src = src, src1 = "111" });
        }
    }
}