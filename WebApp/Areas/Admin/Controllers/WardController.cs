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
    public class WardController : BaseController<WardController>
    {
        private WardService _wardService;
        private DistrictService _districtService;
        public WardController(LogService logService, ILogger<WardController> logger,AuthenUtils authenUtils, WardService wardService, DistrictService districtService) : base(logger,authenUtils, logService)
        {
            _wardService = wardService;
            _districtService = districtService;
        }

        private void GetListDistrict()
        {
            var lstData = new List<SelectListItem>();
            foreach (var item in _districtService.GetAll(x => x.Status == Enums.ActiveStatus.Active))
            {
                lstData.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
            }
            ViewBag.Districts = lstData;
        }

        #region 1.List
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permisson = new PermissionInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_WARD_VIEW))
            {
                permisson.IsView = true;
                permisson.IsEdit = CheckFunctionPermission(Constants.PERMISSION_WARD_EDIT);
                permisson.IsDelete = CheckFunctionPermission(Constants.PERMISSION_WARD_DELETE);
                permisson.IsAdd = CheckFunctionPermission(Constants.PERMISSION_WARD_ADD);
                ViewBag.Permission = permisson;
                var list = _wardService.GetAll(x => x.DeleteStatus != Enums.DeleteStatus.IsDelete).OrderByDescending(o => o.Id);
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
            if (CheckFunctionPermission(Constants.PERMISSION_WARD_ADD))
            {
                GetListDistrict();
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
        public ActionResult Create(WardModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_WARD_ADD))
            {
                if (ModelState.IsValid)
                {
                    _wardService.Add(model);
                    base.SuccessNotification("Thêm mới phường/xã thành công !");
                    return RedirectToAction(nameof(Index));
                }
                GetListDistrict();
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
            if (CheckFunctionPermission(Constants.PERMISSION_WARD_EDIT))
            {
                var data = _wardService.GetById(id);

                if (data != null)
                {
                    GetListDistrict();
                    return View(data.ToWardModel());
                }
                base.ErrorNotification("Không tồn tại phường/xã !");
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
        public ActionResult Edit(WardModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_WARD_EDIT))
            {
                if (ModelState.IsValid)
                {
                    var result = _wardService.Update(model);
                    if (result.IsSuccess())
                    {
                        base.SuccessNotification("Cập nhật phường/xã thành công");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        GetListDistrict();
                        base.ErrorNotification(result.Message);
                        return View(model);
                    }
                }
                else
                {
                    GetListDistrict();
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
            if (CheckFunctionPermission(Constants.PERMISSION_WARD_DELETE))
            {
                message = _wardService.Delete(id);
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
