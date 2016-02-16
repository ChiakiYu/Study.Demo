using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using MvcDemo.Models;
using MvcDemo.Models.DataTables;
using MvcDemo.Models.Geetest;
using Newtonsoft.Json;

namespace MvcDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BaiduMap()
        {
            return PartialView();
        }

        public ContentResult GetData(string name = null)
        {
            var area = new Area();
            IEnumerable<Area> data = null;
            data = string.IsNullOrEmpty(name) ? area.GetData() : area.GetData().Where(n => n.Name == name);
            var result = JsonConvert.SerializeObject(data);
            return Content(result);
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

        public ActionResult UEditor()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UEditor(int i = 1)
        {
            var content = Request.Form.Get("editorValue");

            ViewData["content"] = content;
            return View();
        }

        public ActionResult Plupload()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Upload()
        {
            var nowTime = DateTime.Now;

            var savePath = string.Format("/Yuonhtt_FileUpload/img/{0}/{1}/{2}/", nowTime.Year,
                nowTime.Month.ToString("D2"), nowTime.Day.ToString("D2")); //上传文件的路径 

            var localPath = Server.MapPath(savePath);
            if (!Directory.Exists(Path.GetDirectoryName(localPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(localPath));
            }
            var request = System.Web.HttpContext.Current.Request;
            var src = string.Empty;

            if (request.Files.Count <= 0) return Json(new { src, src1 = "111" });
            for (var i = 0; i < request.Files.Count; i++)
            {
                var file = request.Files[i];
                var extension = Path.GetExtension(file.FileName);
                var fileName = string.Format("{0}{1}",
                    DateTime.Now.ToString("HHmmss") + new Random().Next(100000, 999999), extension);
                file.SaveAs(localPath + fileName);
                src = savePath + fileName;
            }
            return Json(new { src, src1 = "111" });
        }

        public ActionResult ArtDialoig()
        {
            return PartialView();
        }

        public ActionResult Geetest()
        {
            var loginInfo = new LoginInfo();
            return View(loginInfo);
        }

        [HttpPost]
        public ActionResult Geetest(LoginInfo loginInfo)
        {
            if (!ModelState.IsValid)
            {
                ViewData["message"] = "请填写完整数据";
                return View(loginInfo);
            }
            if (!IsVerifyCaptcha())
            {
                ViewData["message"] = "验证码错误";
                return View(loginInfo);
            }
            if (loginInfo.UserName == "admin" && loginInfo.Password == "123123")
            {
                ViewData["message"] ="登录成功";
                return View(loginInfo);
            }
            ViewData["message"] = "用户名密码错误";
            return View(loginInfo);
        }


        public bool IsVerifyCaptcha()
        {
            var geetest = new GeetestLib(GeetestConfig.publicKey, GeetestConfig.privateKey);
            var gtServerStatusCode = (byte)Session[GeetestLib.GtServerStatusSessionKey];
            var challenge = Request.Form.Get(GeetestLib.FnGeetestChallenge);
            var validate = Request.Form.Get(GeetestLib.FnGeetestValidate);
            var seccode = Request.Form.Get(GeetestLib.FnGeetestSeccode);
            var result = gtServerStatusCode == 1
                ? geetest.EnhencedValidateRequest(challenge, validate, seccode)
                : geetest.FailbackValidateRequest(challenge, validate, seccode);
            return result == 1;
        }

        public ContentResult GetCaptcha()
        {
            GeetestLib geetest = new GeetestLib(GeetestConfig.publicKey, GeetestConfig.privateKey);
            Byte gtServerStatus = geetest.PreProcess();
            Session[GeetestLib.GtServerStatusSessionKey] = gtServerStatus;
            return Content(geetest.GetResponseStr());
        }

        public ActionResult DataTables()
        {
            return PartialView();
        }

        public JsonResult GetDatas(AreaQuery query)
        {
            var data = new Area().GetData();
            if (!string.IsNullOrEmpty(query.Name))
                data = data.Where(n => n.Name.Contains(query.Name));
            if (!string.IsNullOrEmpty(query.Description))
                data = data.Where(n => n.Description.Contains(query.Description));
            if (query.X > 0)
                data = data.Where(n => Equals(n.PointX, query.X));
            if (query.Y > 0)
                data = data.Where(n => Equals(n.PointY, query.Y));

            data = data.OrderBy(query.OrderBy, query.OrderDir == DataTablesOrderDir.Desc);
            var count = data.Count();
            var result = data.Skip(query.Start).Take(query.Length).ToList();

            return DataTablesJson(query.Draw, count, count, result);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="draw">请求次数计数器</param>
        /// <param name="recordsTotal">总共记录数</param>
        /// <param name="recordsFiltered">过滤后的记录数</param>
        /// <param name="data">数据</param>
        /// <param name="error">服务器错误信息</param>
        public JsonResult DataTablesJson<TEntity>(int draw, int recordsTotal, int recordsFiltered, IReadOnlyList<TEntity> data, string error = null)
        {
            var result = new DataTablesResult<TEntity>(draw, recordsFiltered, recordsFiltered, data);
            var jsonResult = new JsonResult
            {
                Data = result
            };
            return jsonResult;
        }

        //public JsonResult DataTablesJson(object data)
        //{
        //    var jsonResult = new JsonResult
        //    {
        //        Data = data
        //    };
        //    return jsonResult;
        //}

        //public JsonResult DataTablesJson(object data, JsonRequestBehavior behavior)
        //{
        //    var jsonResult = new JsonResult
        //    {
        //        Data = data,
        //        JsonRequestBehavior = behavior
        //    };
        //    return jsonResult;
        //}

        //public JsonResult DataTablesJson(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        //{
        //    var jsonResult = new JsonResult
        //    {
        //        Data = data,
        //        ContentType = contentType,
        //        ContentEncoding = contentEncoding,
        //        JsonRequestBehavior = behavior
        //    };
        //    return jsonResult;
        //}
    }

    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, string propertyName)
        {
            return QueryableHelper<T>.OrderBy(queryable, propertyName, false);
        }
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, string propertyName, bool desc)
        {
            return QueryableHelper<T>.OrderBy(queryable, propertyName, desc);
        }
        static class QueryableHelper<T>
        {
            private static ConcurrentDictionary<string, LambdaExpression> cache = new ConcurrentDictionary<string, LambdaExpression>();
            public static IQueryable<T> OrderBy(IQueryable<T> queryable, string propertyName, bool desc)
            {
                dynamic keySelector = GetLambdaExpression(propertyName);
                return desc ? Queryable.OrderByDescending(queryable, keySelector) : Queryable.OrderBy(queryable, keySelector);
            }
            private static LambdaExpression GetLambdaExpression(string propertyName)
            {
                if (cache.ContainsKey(propertyName))
                    return cache[propertyName];
                var param = Expression.Parameter(typeof(T));
                var body = Expression.Property(param, propertyName);
                var keySelector = Expression.Lambda(body, param);
                cache[propertyName] = keySelector;
                return keySelector;
            }
        }
    }
}