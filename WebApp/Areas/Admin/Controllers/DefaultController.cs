using Common;
using Datas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services;
using WebApp.Common;

namespace WebApp.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route(nameof(Admin))]
    [Route(nameof(Admin) + "/[controller]")]
    public class DefaultController : BaseController<DefaultController>
    {
        private SettingUtils _settingUtils;
        public DefaultController(LogService logService, ILogger<DefaultController> logger, AuthenUtils authenUtils, SettingUtils settingUtils) : base(logger, authenUtils, logService)
        {
            _settingUtils = settingUtils;
        }

        [Route("Index")]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Setting")]
        public ActionResult Setting()
        {
            if (CheckFunctionPermission(Constants.PERMISSION_SETTING))
            {
                _settingUtils.Read();
                return View(_settingUtils.Setting);
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }

        }

        [HttpPost]
        [Route("Setting")]
        public ActionResult Setting(SettingModel setting)
        {
            if (CheckFunctionPermission(Constants.PERMISSION_SETTING))
            {
                if (_settingUtils.Save(setting))
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
