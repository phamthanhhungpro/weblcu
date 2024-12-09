using Common;
using Common.Entity;
using Common.Entity.Permission;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Services;
using System.Linq.Expressions;
using WebApp.Common;

namespace WebApp.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route(nameof(Admin) + "/[controller]")]
    public class LogController : BaseController<NewsController>
    {
        private LogService _logService;
        private AdminSettingUtils _adminSettingUtils;
        public LogController(ILogger<NewsController> logger, AuthenUtils authenUtils, LogService logService, AdminSettingUtils  adminSettingUtils) : base(logger, authenUtils,logService)
        {
            _logService = logService;
            _adminSettingUtils = adminSettingUtils;
        }

        // GET: Admin/News
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permission = new PermissionInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_LOG_VIEW))
            {
                permission.IsView = true;
                ViewBag.Permission = permission;
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        [Route("LoadAjaxNew")]
        public JsonResult LoadAjaxNew()
        {
            MessageResult message = new MessageResult();
            var start = 0;
            var length = 10;
            var term = HttpContext.Request.Query["search[value]"].ToString();
            var startParam = HttpContext.Request.Query["start"].ToString();
            var lengthParam = HttpContext.Request.Query["length"].ToString();
            var orderDir = HttpContext.Request.Query["order[0][dir]"].ToString();
            var orderColumnIndex = HttpContext.Request.Query["order[0][column]"].ToString();
            var orderColumnName = HttpContext.Request.Query["columns[" + orderColumnIndex + "][data]"].ToString();
            IEnumerable<LogData> list = null;
            if (term != null)
            {
                term = term.ToLower();
            }
            try
            {
                start = Int32.Parse(startParam);
                length = Int32.Parse(lengthParam);

            }
            catch (Exception)
            {
            }
            var totalRecord = 0;
            var total = _logService.GetAll().Count();
            if (CheckFunctionPermission(Constants.PERMISSION_LOG_VIEW))
            {

                if (!string.IsNullOrEmpty(term))
                {
                    list = _logService.GetAll(e => e.Id.ToLower().Contains(term) || e.UserId.ToString().ToLower().Contains(term) || e.UserName.ToLower().Contains(term) || e.FullName.ToLower().Contains(term) || (!string.IsNullOrEmpty(e.Controller) && e.Controller.ToLower().Contains(term)) || (!string.IsNullOrEmpty(e.Action) && e.Action.ToLower().Contains(term)) || (!string.IsNullOrEmpty(e.Query) && e.Query.ToLower().Contains(term)) || e.InsertDate.ToString("dd/MM/yyyy HH:mm:ss").ToLower().Contains(term));
                }
                else
                {
                    list = _logService.GetAll();
                }
                bool isSort = true;
                Func<LogData, object> sortData = null;
                switch (orderColumnName)
                {
                    case "id":
                        sortData = o => o.Id;
                        break;
                    case "insertDate":
                        sortData = o => o.InsertDate;
                        break;
                    case "userId":
                        sortData = o => o.UserId;
                        break;
                    case "userName":
                        sortData = o => o.UserName;
                        break;
                    case "fullName":
                        sortData = o => o.FullName;
                        break;
                    case "ip":
                        sortData = o => o.Ip;
                        break;
                    case "method":
                        sortData = o => o.Method;
                        break;
                    case "controller":
                        sortData = o => o.Controller;
                        break;
                    case "action":
                        sortData = o => o.Action;
                        break;
                    case "query":
                        sortData = o => o.Query;
                        break;
                    default:
                        isSort = false;
                        break;
                }
                if (isSort)
                {
                    if (orderDir.Equals("asc"))
                        list = list.OrderBy(sortData);
                    else
                        list = list.OrderByDescending(sortData);
                }
                totalRecord = list.Count();
            }
            var result = new
            {
                recordsTotal = total,
                recordsFiltered = totalRecord,
                aaData = list != null ? BindData(list, start, length) : null
            };
            var jsonResult = Json(result);
            return jsonResult;
        }

        private Object BindData(IEnumerable<LogData> list, int start, int length)
        {
            var result = new List<object>();
            var index = start + 1;

            foreach (var item in list.Skip(start).Take(length))
            {
                result.Add(new
                {
                    item.Id,
                    Index = index,
                    InsertDate = item.InsertDate.ToString("dd/MM/yyyy HH:mm:ss"),
                    item.UserId,
                    item.UserName,
                    item.FullName,
                    item.Method,
                    item.Ip,
                    Controller = item.Controller,
                    Action = item.Action,
                    Query = item.Query,
                });
                index++;
            }
            return result;
        }

        [HttpGet]
        [Route("Setting")]
        public ActionResult Setting()
        {
            if (CheckFunctionPermission(Constants.PERMISSION_LOG_SETTING))
            {
                _adminSettingUtils.Read();
                return View(_adminSettingUtils.Setting);
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }

        [HttpPost]
        [Route("Setting")]
        public ActionResult Setting(AdminSettingModel setting)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_LOG_SETTING))
            {
                setting.BlockIps = setting.BlockIps.Distinct().Where(o=>!string.IsNullOrEmpty(o)).ToList();
                if (_adminSettingUtils.Save(setting))
                    base.SuccessNotification("Cập nhật thành công");
                else
                    base.ErrorNotification("Cập nhật thất bại");
                return View(setting);
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

    }
}
