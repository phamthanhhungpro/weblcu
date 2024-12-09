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
    public class DistrictController : BaseController<DistrictController>
    {
        private DistrictService _districtService;
        public DistrictController(LogService logService, ILogger<DistrictController> logger,AuthenUtils authenUtils, DistrictService districtService) : base(logger,authenUtils, logService)
        {
            _districtService = districtService;
        }
        #region 1.List
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permisson = new PermissionInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_DISTRICT_VIEW))
            {
                permisson.IsView = true;
                permisson.IsEdit = CheckFunctionPermission(Constants.PERMISSION_DISTRICT_EDIT);
                permisson.IsDelete = CheckFunctionPermission(Constants.PERMISSION_DISTRICT_DELETE);
                permisson.IsAdd = CheckFunctionPermission(Constants.PERMISSION_DISTRICT_ADD);
                ViewBag.Permission = permisson;
                var list = _districtService.GetAll(x => x.DeleteStatus != Enums.DeleteStatus.IsDelete).OrderByDescending(o => o.Id);
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
            if (CheckFunctionPermission(Constants.PERMISSION_DISTRICT_ADD))
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
        public ActionResult Create(DistrictModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_DISTRICT_ADD))
            {
                if (ModelState.IsValid)
                {
                    _districtService.Add(model);
                    base.SuccessNotification("Thêm mới thành phố/huyện thành công !");
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
            if (CheckFunctionPermission(Constants.PERMISSION_DISTRICT_EDIT))
            {
                var data = _districtService.GetById(id);

                if (data != null)
                {
                    return View(data.ToDistrictModel());
                }
                base.ErrorNotification("Không tồn tại thành phố/huyện !");
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
        public ActionResult Edit(DistrictModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_DISTRICT_EDIT))
            {
                if (ModelState.IsValid)
                {
                    var result = _districtService.Update(model);
                    if (result.IsSuccess())
                    {
                        base.SuccessNotification("Cập nhật thành phố/huyện thành công");
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
            if (CheckFunctionPermission(Constants.PERMISSION_DISTRICT_DELETE))
            {
                message = _districtService.Delete(id);
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
