using Common.Entity.Permission;
using Common.Entity;
using Common;
using Microsoft.AspNetCore.Mvc;
using Services;
using WebApp.Common;

namespace WebApp.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route(nameof(Admin) + "/[controller]")]
    public class ContactController : BaseController<ContactController>
    {
        private ContactService _contactService;
        public ContactController(LogService logService,ContactService contactService, AuthenUtils authen,ILogger<ContactController> logger) : base(logger, authen, logService)
        {
            _contactService = contactService;
        }

        #region 1.List
        [Route("")]
        [Route("Index")]
        public ActionResult Index()
        {
            var permission = new PermissionInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_CONTACT_VIEW))
            {
                permission.IsView = true;
                permission.IsDelete = CheckFunctionPermission(Constants.PERMISSION_CONTACT_DELETE);
                ViewBag.Permission = permission;
                var list = _contactService.GetAll(x => x.DeleteStatus != Enums.DeleteStatus.IsDelete).OrderByDescending(o => o.Id);
                return View(list);
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }
        #endregion

        #region 5.Delete
        [HttpPost]
        [Route("Delete")]
        public JsonResult Delete(int id)
        {
            MessageResult message = new MessageResult();
            if (CheckFunctionPermission(Constants.PERMISSION_CONTACT_DELETE))
            {
                message = _contactService.Delete(id);
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
