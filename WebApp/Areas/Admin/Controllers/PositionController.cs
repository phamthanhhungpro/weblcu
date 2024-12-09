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
    public class PositionController : BaseController<PositionController>
    {
        private PositionService _positionService;
        public PositionController(LogService logService, ILogger<PositionController> logger,AuthenUtils authenUtils, PositionService positionService) : base(logger,authenUtils, logService)
        {
            _positionService = positionService;
        }
        #region 1.List
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permisson = new PermissionInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_POSITION_VIEW))
            {
                permisson.IsView = true;
                permisson.IsEdit = CheckFunctionPermission(Constants.PERMISSION_POSITION_EDIT);
                permisson.IsDelete = CheckFunctionPermission(Constants.PERMISSION_POSITION_DELETE);
                permisson.IsAdd = CheckFunctionPermission(Constants.PERMISSION_POSITION_ADD);
                ViewBag.Permission = permisson;
                var list = _positionService.GetAll(x => x.DeleteStatus != Enums.DeleteStatus.IsDelete).OrderByDescending(o => o.Id);
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
            if (CheckFunctionPermission(Constants.PERMISSION_POSITION_ADD))
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
        public ActionResult Create(PositionModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_POSITION_ADD))
            {
                if (ModelState.IsValid)
                {
                    _positionService.Add(model);
                    base.SuccessNotification("Thêm mới chức vụ thành công !");
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
            if (CheckFunctionPermission(Constants.PERMISSION_POSITION_EDIT))
            {
                var positioninfor = _positionService.GetById(id);

                if (positioninfor != null)
                {
                    return View(positioninfor.ToPositionModel());
                }
                base.ErrorNotification("Không tồn tại chức vụ !");
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
        public ActionResult Edit(PositionModel model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_POSITION_EDIT))
            {
                if (ModelState.IsValid)
                {
                    var result = _positionService.Update(model);
                    if (result.IsSuccess())
                    {
                        base.SuccessNotification("Cập nhật chức vụ thành công");
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
            if (CheckFunctionPermission(Constants.PERMISSION_POSITION_DELETE))
            {
                message = _positionService.Delete(id);
            }
            else
            {
                message.Code = Enums.ErrorCode.PositionNotPermisson;
            }
            return Json(message);
        }
        #endregion
    }

}
