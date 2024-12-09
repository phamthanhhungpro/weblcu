using Common;
using Common.Entity.Permission;
using Microsoft.AspNetCore.Mvc;
using Services;
using WebApp.Common;

namespace WebApp.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route(nameof(Admin) + "/[controller]")]
    public class InstrucmentCategoryController : BaseController<NewsCategoryController>
    {
        public InstrucmentCategoryController(LogService logService, ILogger<NewsCategoryController> logger, AuthenUtils authenUtils) : base(logger, authenUtils, logService)
        {
        }

        [HttpGet]
        [Route("Index")]
        [Route("")]
        public ActionResult Index()
        {
            var permission = new PermissionInfo();
            if (CheckFunctionPermission(Constants.PERMISSION_INSTRUCMENT_CATEGORY_VIEW))
            {
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }
    }
}
