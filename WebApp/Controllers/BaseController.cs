using Common;
using Common.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.OutputCaching;
using Services;
using WebApp.Common;

namespace WebApp.Controllers
{
    public class BaseController : Controller
    {
        private PeopleCategoryService _peopleCategoryService;
        public SettingUtils Settings { get; }
        public BaseController(PeopleCategoryService peopleCategoryService, SettingUtils settingUtils)
        {
            _peopleCategoryService = peopleCategoryService;
            Settings = settingUtils;
        }

        [OutputCache(PolicyName = "10Minute")]
        protected void BuildMenu()
        {
            List<MenuItem> lstMenu = new List<MenuItem>();
            lstMenu.AddRange(BuildPeopleCategoryMenu());
            ViewBag.Menu = lstMenu;
        }

        private List<MenuItem> BuildPeopleCategoryMenu()
        {
            List<MenuItem> lstMenu = new List<MenuItem>();
            var data = _peopleCategoryService.GetAll(o => o.Status == Enums.ActiveStatus.Active && !o.ParentId.HasValue).ToList();
            foreach (var item in data)
            {
                lstMenu.Add(new MenuItem
                {
                    Name = item.Name,
                    Url = item.Url
                });
            }
            return lstMenu;
        }

        protected virtual void SuccessNotification<T>(MessageResult<T> message, bool persistForTheNextRequest = true)
        {
            AddNotification("Success", message.Message, persistForTheNextRequest);
        }

        protected virtual void ErrorNotification<T>(MessageResult<T> message, bool persistForTheNextRequest = true)
        {
            AddNotification("Error", message, persistForTheNextRequest);
        }

        protected virtual void SuccessNotification(string message, bool persistForTheNextRequest = true)
        {
            AddNotification("Success", message, persistForTheNextRequest);
        }

        protected virtual void ErrorNotification(string message, bool persistForTheNextRequest = true)
        {
            AddNotification("Error", message, persistForTheNextRequest);
        }

        protected virtual void ErrorNotification(Exception exception, bool persistForTheNextRequest = true)
        {
            AddNotification("Error", exception.Message, persistForTheNextRequest);
        }

        private void AddNotification<T>(string type, MessageResult<T> message, bool persistForTheNextRequest)
        {
            AddNotification(type, string.Format("Mã lỗi {0}: {1}", message.Code, message.Message), persistForTheNextRequest);
        }

        private void AddNotification(string type, string message, bool persistForTheNextRequest)
        {
            var dataKey = string.Format("notifications.{0}", type);
            if (persistForTheNextRequest)
            {
                if (TempData[dataKey] == null)
                    TempData[dataKey] = new List<string>();
                ((List<string>)TempData[dataKey]).Add(message);
            }
            else
            {
                if (ViewData[dataKey] == null)
                    ViewData[dataKey] = new List<string>();
                ((List<string>)ViewData[dataKey]).Add(message);
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //BuildMenu();
            ViewBag.Setting = Settings.Setting;
            ViewBag.SubDomain = Settings.SubDomain;
        }
    }
}
