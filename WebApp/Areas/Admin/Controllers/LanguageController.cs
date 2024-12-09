using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Datas;
using Datas.Models.DomainModels;
using Common.Entity.Permission;
using Common.Entity;
using Common;
using Datas.Models.ViewModels;
using Services;
using WebApp.Common;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route(nameof(Admin) + "/[controller]")]
    public class LanguageController : BaseController<LanguageController>
    {
        private readonly LanguageService _languageService;
        public LanguageController(LogService logService, ILogger<LanguageController> logger,AuthenUtils authenUtils, LanguageService languageService) : base(logger,authenUtils, logService)
        {
            _languageService = languageService;
        }
        #region 1.List
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permisson = new PermissionInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_LANGUAGE_VIEW))
            {
                permisson.IsView = true;
                permisson.IsEdit = CheckFunctionPermission(Constants.PERMISSION_LANGUAGE_EDIT);
                permisson.IsDelete = CheckFunctionPermission(Constants.PERMISSION_LANGUAGE_DELETE);
                permisson.IsAdd = CheckFunctionPermission(Constants.PERMISSION_LANGUAGE_ADD);
                ViewBag.Permission = permisson;
                var list = _languageService.GetAll(x => x.DeleteStatus != Enums.DeleteStatus.IsDelete).OrderByDescending(o => o.Id);
                return View(list);
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }
        #endregion
        #region 2.Create
        [HttpGet]
        [Route("Create")]
        public ActionResult Create()
        {
            if (CheckFunctionPermission(Constants.PERMISSION_LANGUAGE_ADD))
            {
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(LanguageModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_LANGUAGE_ADD))
            {
                if (ModelState.IsValid)
                {
                    _languageService.Add(model);
                    base.SuccessNotification("Thêm mới ngôn ngữ thành công !");
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }


        }
        #endregion
        #region 3. Edit
        [HttpGet]
        [Route("Edit")]
        public ActionResult Edit(int id)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_LANGUAGE_EDIT))
            {
                var data = _languageService.GetById(id);

                if (data != null)
                {
                    return View(data.ToLanguageModel());
                }
                base.ErrorNotification("Không tồn tại ngôn ngữ !");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }
        [HttpPost]
        [Route("Edit")]
        public ActionResult Edit(LanguageModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_LANGUAGE_EDIT))
            {
                if (ModelState.IsValid)
                {
                    var result = _languageService.Update(model);
                    if (result.IsSuccess())
                    {
                        base.SuccessNotification("Cập nhật ngôn ngữ thành công");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        base.ErrorNotification(result.Message);
                        return View(model);
                    }
                }
                else
                {
                    return View(model);
                }

            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }


        }
        #endregion
        #region 4.GetList
        #endregion
        #region 5.Delete
        [HttpPost]
        [Route("Delete")]
        public JsonResult Delete(int id)
        {
            MessageResult message = new MessageResult();
            if (CheckFunctionPermission(Constants.PERMISSION_LANGUAGE_DELETE))
            {
                message = _languageService.Delete(id);
            }
            else
            {
                message.Code = Enums.ErrorCode.OtherNotPermisson;
            }
            return Json(message);
        }
        #endregion
    }

}
