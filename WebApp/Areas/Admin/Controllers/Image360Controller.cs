using Common.Entity.Permission;
using Common.Entity;
using Common;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services;
using WebApp.Common;

namespace WebApp.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route(nameof(Admin) + "/[controller]")]
    public class Image360Controller : BaseController<Image360Controller>
    {
        private Image360Service _image360Service;
        public Image360Controller(LogService logService, ILogger<Image360Controller> logger, AuthenUtils authenUtils, Image360Service image360Service) : base(logger, authenUtils, logService)
        {
            _image360Service = image360Service;
        }

        #region 1.List
        [Route("Index")]
        public ActionResult Index()
        {
            var permisson = new PermissionInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_IMAGE_VIEW))
            {
                permisson.IsView = true;
                permisson.IsEdit = CheckFunctionPermission(Constants.PERMISSION_IMAGE_EDIT);
                permisson.IsDelete = CheckFunctionPermission(Constants.PERMISSION_IMAGE_DELETE);
                permisson.IsAdd = CheckFunctionPermission(Constants.PERMISSION_IMAGE_ADD);
                ViewBag.Permission = permisson;
                var list = _image360Service.GetAll(x => x.DeleteStatus != Enums.DeleteStatus.IsDelete).OrderByDescending(o => o.Id);
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
            if (CheckFunctionPermission(Constants.PERMISSION_IMAGE_ADD))
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
        public ActionResult Create(Image360 model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_IMAGE_ADD))
            {
                if (ModelState.IsValid)
                {
                    _image360Service.Add(model);
                    base.SuccessNotification("Thêm mới ảnh 360 thành công !");
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
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
            if (CheckFunctionPermission(Constants.PERMISSION_IMAGE_EDIT))
            {
                var image360 = _image360Service.GetById(id);

                if (image360 != null)
                {
                    return View(image360.ToImage360Model());
                }
                base.ErrorNotification("Không tồn tại ảnh 360 !");
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
        public ActionResult Edit(Image360Model model)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_IMAGE_EDIT))
            {
                if (ModelState.IsValid)
                {
                    var result = _image360Service.Update(model);
                    if (result.IsSuccess())
                    {
                        base.SuccessNotification("Cập nhật ảnh 360 thành công");
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
                    ModelState.AddModelError("", "Bạn chưa nhập đủ thông tin bắt buộc");
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
            if (CheckFunctionPermission(Constants.PERMISSION_IMAGE_DELETE))
            {
                message = _image360Service.Delete(id);
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
