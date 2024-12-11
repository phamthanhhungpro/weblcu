using Common.Entity;
using Common;
using Datas.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApp.Common;
using WebApp.Controllers;
using Newtonsoft.Json;
using Datas.Models.DomainModels;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Http.Extensions;
using System.Security.Policy;
using System;
using Services;

namespace WebApp.Areas.Admin.Controllers
{
    public class BaseController<T>(ILogger<T> logger, AuthenUtils authenUtils,LogService logService) : Controller
    {
        public readonly ILogger<T> Log = logger;
        private ILogger<ContactController> logger;

        public AuthenUtils Authen { get; private set; } = authenUtils;
        public UserInfo UserData { get; set; }
        public LogService LogService { get; private set; } = logService;

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
            var userInfo = Authen.GetSessionUserInfo();
            if (userInfo != null)
            {

                UserData = userInfo;
         
                if (!Request.IsAjaxRequest())
                {
                    ViewBag.UserInfo = userInfo;
                    TempData["UserInfo"] = 1;
                    HttpContext?.Session.SetString(Constants.SESSION_USER_INFO, JsonConvert.SerializeObject(userInfo));
                    BuildMenu(((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor).ControllerName);
                }
                WriteLogAction(userInfo, (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor);
                base.OnActionExecuting(filterContext);
            }
            else
            {
                RouteValueDictionary route = new RouteValueDictionary(new
                {
                    Area = "Admin",
                    Controller = "Login",
                    Action = "Index"

                });
                filterContext.Result = new RedirectToRouteResult(route);
            }
        }

        private void WriteLogAction(UserInfo userInfo, Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor controllerActionDescriptor)
        {
            try
            {
                var strData = string.Empty;
                if (!string.IsNullOrEmpty(Request.ContentType))
                {
                    var formData = Request.Form;
                    if (formData.Count > 0)
                    {
                        strData = JsonConvert.SerializeObject(formData);
                    }
                }
                else if (Request.Query.Count > 20)
                {
                    strData = JsonConvert.SerializeObject(Request.Query.Where(o => !o.Key.Contains("columns[")).ToList());
                }
                else if (Request.Query.Count > 0)
                {
                    strData = JsonConvert.SerializeObject(Request.Query.ToList());
                }

                var ip = HttpContext?.Request.Headers["X-Forwarded-For"].ToString();
                if (string.IsNullOrEmpty(ip))
                {
                    ip = HttpContext?.Connection?.RemoteIpAddress?.ToString();
                }

                LogService.Add(new Datas.Models.DomainModels.LogData
                {
                    UserId = userInfo.Id,
                    UserName = userInfo.UserName,
                    FullName = userInfo.FullName,
                    Method = Request.Method,
                    Controller = controllerActionDescriptor.ControllerName,
                    Action = controllerActionDescriptor.ActionName,
                    Query = strData,
                    Ip = ip
                });;
            }
            catch
            {

            }
            
        }

        private void BuildMenu(string controllerName)
        {
            List<MenuItem> lstMenu = new List<MenuItem>();
            var url = Request.Path;

            try
            {
                var lstGroup = Authen.GetListGroupFunction();
                var functionResult = Authen.GetFunctionsByUserId(UserData.Id);
                if (functionResult.IsSuccess())
                {
                    foreach (var group in lstGroup.OrderBy(o => o.Order))
                    {
                        switch (group.Type)
                        {
                            case Enums.GroupFunctionType.Department:
                                var departmentMenu = CreateDepartmentMenu(group, functionResult.Value, url);
                                if (controllerName.Equals("Derpartment") && departmentMenu != null)
                                    departmentMenu.IsOpen = true;
                                if (departmentMenu != null)
                                    lstMenu.Add(departmentMenu);
                                break;
                            case Enums.GroupFunctionType.Position:
                                var positionMenu = CreatePositionMenu(group, functionResult.Value, url);
                                if (controllerName.Equals("Position") && positionMenu != null)
                                    positionMenu.IsOpen = true;
                                if (positionMenu != null)
                                    lstMenu.Add(positionMenu);
                                break;
                            case Enums.GroupFunctionType.Contact:
                                var contactMenu = CreateContactMenu(group, functionResult.Value, url);
                                if (controllerName.Equals("Contact") && contactMenu != null)
                                    contactMenu.IsOpen = true;
                                if (contactMenu != null)
                                    lstMenu.Add(contactMenu);
                                break;
                            case Enums.GroupFunctionType.Setting:
                                var settingMenu = CreateSettingMenu(group, functionResult.Value, url);
                                if (controllerName.Equals("Default") && settingMenu != null)
                                    settingMenu.IsOpen = true;
                                if (settingMenu != null)
                                    lstMenu.Add(settingMenu);
                                break;
                            case Enums.GroupFunctionType.Log:
                                var logMenu = CreateLogMenu(group, functionResult.Value, url);
                                if (controllerName.Equals("Log") && logMenu != null)
                                    logMenu.IsOpen = true;
                                if (logMenu != null)
                                    lstMenu.Add(logMenu);
                                break;
                            case Enums.GroupFunctionType.User:
                                var userMenu = CreateUserMenu(group, functionResult.Value, url);
                                if (controllerName.Equals("User") && userMenu != null)
                                    userMenu.IsOpen = true;
                                if (userMenu != null)
                                    lstMenu.Add(userMenu);
                                break;
                            case Enums.GroupFunctionType.Company:
                                var companyMenu = CreateCompanyMenu(group, functionResult.Value, url);
                                if (controllerName.Equals("Company") && companyMenu != null)
                                    companyMenu.IsOpen = true;
                                if (companyMenu != null)
                                    lstMenu.Add(companyMenu);
                                break;
                            case Enums.GroupFunctionType.NewsCategory:
                                var newsCategoryMenu = CreateNewsCategoryMenu(group, functionResult.Value,url);
                                if (controllerName.Equals("NewsCategory") && newsCategoryMenu != null)
                                    newsCategoryMenu.IsOpen = true;
                                if (newsCategoryMenu != null)
                                    lstMenu.Add(newsCategoryMenu);
                                break;
                            case Enums.GroupFunctionType.News:
                                var newsMenu = CreateNewsMenu(group, functionResult.Value, url);
                                if (controllerName.Equals("News") && newsMenu != null)
                                    newsMenu.IsOpen = true;
                                if (newsMenu != null)
                                    lstMenu.Add(newsMenu);
                                break;
                            //case Enums.GroupFunctionType.PeopleCategory:
                            //    var peopleCategoryMenu = CreatePeopleCategoryMenu(group, functionResult.Value, url);
                            //    if (controllerName.Equals("PeopleCategory") && peopleCategoryMenu != null)
                            //        peopleCategoryMenu.IsOpen = true;
                            //    if (peopleCategoryMenu != null)
                            //        lstMenu.Add(peopleCategoryMenu);
                            //    break;
                            case Enums.GroupFunctionType.People:
                                var peopleMenu = CreatePeopleMenu(group, functionResult.Value, url);
                                if (controllerName.Equals("People") && peopleMenu != null)
                                    peopleMenu.IsOpen = true;
                                if (peopleMenu != null)
                                    lstMenu.Add(peopleMenu);
                                break;
                            case Enums.GroupFunctionType.NationalCostumeCategory:
                                var categoryMenu = CreateNationalCostumeCategoryMenu(group, functionResult.Value, url);
                                if (controllerName.Equals("NationalCostumeCategory") && categoryMenu != null)
                                    categoryMenu.IsOpen = true;
                                if (categoryMenu != null)
                                    lstMenu.Add(categoryMenu);
                                break;
                            case Enums.GroupFunctionType.NationalCostume:
                                var nationalCostumeMenu = CreateNationalCostumeMenu(group, functionResult.Value, url);
                                if (controllerName.Equals("NationalCostume") && nationalCostumeMenu != null)
                                    nationalCostumeMenu.IsOpen = true;
                                if (nationalCostumeMenu != null)
                                    lstMenu.Add(nationalCostumeMenu);
                                break;
                            case Enums.GroupFunctionType.Role:
                                var roleMenu = CreateRoleMenu(group, functionResult.Value, url);
                                if (controllerName.Equals("Role") && roleMenu != null)
                                    roleMenu.IsOpen = true;
                                if (roleMenu != null)
                                    lstMenu.Add(roleMenu);
                                break;
                            case Enums.GroupFunctionType.File:
                                var fileMenu = CreateFileMenu(group, functionResult.Value, url);
                                if (controllerName.Equals("File") && fileMenu != null)
                                    fileMenu.IsOpen = true;
                                if (fileMenu != null)
                                    lstMenu.Add(fileMenu);
                                break;
                            case Enums.GroupFunctionType.Image:
                                var imageMenu = CreateImageMenu(group, functionResult.Value, url);
                                if (controllerName.Equals("Image360") && imageMenu != null)
                                    imageMenu.IsOpen = true;
                                if (imageMenu != null)
                                    lstMenu.Add(imageMenu);
                                break;
                            case Enums.GroupFunctionType.Report:
                                var reportMenu = CreateReportMenu(group, functionResult.Value, url);
                                if (controllerName.Equals("Report") && reportMenu != null)
                                    reportMenu.IsOpen = true;
                                if (reportMenu != null)
                                    lstMenu.Add(reportMenu);
                                break;
                            case Enums.GroupFunctionType.District:
                                var districtMenu = CreateDistrictMenu(group, functionResult.Value, url);
                                if (controllerName.Equals("District") && districtMenu != null)
                                    districtMenu.IsOpen = true;
                                if (districtMenu != null)
                                    lstMenu.Add(districtMenu);
                                break;
                            case Enums.GroupFunctionType.Ward:
                                var wardMenu = CreateWardMenu(group, functionResult.Value, url);
                                if (controllerName.Equals("Ward") && wardMenu != null)
                                    wardMenu.IsOpen = true;
                                if (wardMenu != null)
                                    lstMenu.Add(wardMenu);
                                break;
                            case Enums.GroupFunctionType.Language:
                                var languageMenu = CreateLanguageMenu(group, functionResult.Value, url);
                                if (controllerName.Equals("Language") && languageMenu != null)
                                    languageMenu.IsOpen = true;
                                if (languageMenu != null)
                                    lstMenu.Add(languageMenu);
                                break;
                            case Enums.GroupFunctionType.InstrumentCategory:
                                AddMenuItem(ref lstMenu,
                                            controllerName,
                                            "InstrumentCategory",
                                            group,
                                            functionResult.Value,
                                            url,
                                            Constants.PERMISSION_INSTRUMENT_CATEGORY_VIEW,
                                            Constants.PERMISSION_INSTRUMENT_CATEGORY_ADD,
                                            "fa fa-music");
                                break;
                            case Enums.GroupFunctionType.Instrument:
                                AddMenuItem(ref lstMenu,
                                    controllerName,
                                    "Instrument",
                                    group,
                                    functionResult.Value,
                                    url,
                                    Constants.PERMISSION_INSTRUMENT_VIEW,
                                    Constants.PERMISSION_INSTRUMENT_ADD,
                                    "fa fa-music");
                                break;
                            case Enums.GroupFunctionType.ProduceToolCategory:
                                AddMenuItem(ref lstMenu,
                                            controllerName,
                                            "ProduceToolCategory",
                                            group,
                                            functionResult.Value,
                                            url,
                                            Constants.PERMISSION_PRODUCETOOL_CATEGORY_VIEW,
                                            Constants.PERMISSION_PRODUCETOOL_CATEGORY_ADD,
                                            "fa fa-wrench");
                                break;
                            case Enums.GroupFunctionType.ProduceTool:
                                AddMenuItem(ref lstMenu,
                                            controllerName,
                                            "ProduceTool",
                                            group,
                                            functionResult.Value,
                                            url,
                                            Constants.PERMISSION_PRODUCETOOL_VIEW,
                                            Constants.PERMISSION_PRODUCETOOL_ADD,
                                            "fa fa-wrench");
                                break;

                            case Enums.GroupFunctionType.CustomsTradition:
                                AddMenuItem(ref lstMenu,
                                            controllerName,
                                            "CustomsTradition",
                                            group,
                                            functionResult.Value,
                                            url,
                                            Constants.PERMISSION_CUSTOMSTRADITION_VIEW,
                                            Constants.PERMISSION_CUSTOMSTRADITION_ADD,
                                            "fa fa-feather-alt");
                                break;

                            case Enums.GroupFunctionType.Festival:
                                AddMenuItem(ref lstMenu,
                                            controllerName,
                                            "Festival",
                                            group,
                                            functionResult.Value,
                                            url,
                                            Constants.PERMISSION_FESTIVAL_VIEW,
                                            Constants.PERMISSION_FESTIVAL_ADD,
                                            "fa fa-holly-berry");
                                break;
                            
                            case Enums.GroupFunctionType.Landmark:
                                AddMenuItem(ref lstMenu,
                                    controllerName,
                                    "Landmark",
                                    group,
                                    functionResult.Value,
                                    url,
                                    Constants.PERMISSION_LANDMARK_VIEW,
                                    Constants.PERMISSION_LANDMARK_ADD,
                                    "fa fa-landmark");
                                break;
                            default:
                                break;

                        }
                    }
                }
                else
                {
                    Log.LogWarning(functionResult.Message);
                }
            }
            catch(Exception ex)
            {
                Log.LogError(ex, "");
            }

            ViewBag.Menu = lstMenu;
        }

        private void AddMenuItem(ref List<MenuItem> lstMenu,
                                string currentControllerName,
                                string menuControllerName,
                                GroupFunction groupFunction,
                                List<Function> funcs,
                                string currentUrl,
                                string viewPermission,
                                string addPermission,
                                string iconClass)
        {
            var menu = CreateMenuItem(menuControllerName, groupFunction, funcs, currentUrl,
                viewPermission, addPermission, iconClass);

            if (currentControllerName.Equals(menuControllerName) && menu != null)
            {
                menu.IsOpen = true;
            }
            if (menu != null)
            {
                lstMenu.Add(menu);
            }
        }

        private MenuItem CreateFileMenu(GroupFunction group, List<Function> functions, string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_FILE_VIEW, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách", Url = Url.Action("Index", "File"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_FILE_ADD, functions))
                lstMenu.Add(new MenuItem { Name = "Thêm mới", Url = Url.Action("Create", "File"), ClassCss = "fa fa-caret-right" });
            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = "fa fa-file-alt", IsOpen = isOpen };
            }
            return null;
        }

        private MenuItem CreateWardMenu(GroupFunction group, List<Function> functions,string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_WARD_VIEW, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách", Url = Url.Action("Index", "Ward"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_WARD_ADD, functions))
                lstMenu.Add(new MenuItem { Name = "Thêm mới", Url = Url.Action("Create", "Ward"), ClassCss = "fa fa-caret-right" });
            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = "fa fa-location-arrow", IsOpen = isOpen };
            }
            return null;
        }

        private MenuItem CreateDistrictMenu(GroupFunction group, List<Function> functions, string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_DISTRICT_VIEW, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách", Url = Url.Action("Index", "District"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_DISTRICT_ADD, functions))
                lstMenu.Add(new MenuItem { Name = "Thêm mới", Url = Url.Action("Create", "District"), ClassCss = "fa fa-caret-right" });
            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = "fa fa-map-marked-alt", IsOpen = isOpen };
            }
            return null;
        }

        private MenuItem CreateLanguageMenu(GroupFunction group, List<Function> functions, string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_LANGUAGE_VIEW, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách", Url = Url.Action("Index", "Language"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_LANGUAGE_ADD, functions))
                lstMenu.Add(new MenuItem { Name = "Thêm mới", Url = Url.Action("Create", "Language"), ClassCss = "fa fa-caret-right" });
            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = "fa fa-language", IsOpen = isOpen };
            }
            return null;
        }

        private MenuItem CreateImageMenu(GroupFunction group, List<Function> functions, string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_IMAGE_VIEW, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách", Url = Url.Action("Index", "Image360"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_IMAGE_ADD, functions))
                lstMenu.Add(new MenuItem { Name = "Thêm mới", Url = Url.Action("Create", "Image360"), ClassCss = "fa fa-caret-right" });
            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = "fas fa-images", IsOpen = isOpen };
            }
            return null;
        }

        private MenuItem CreateReportMenu(GroupFunction group, List<Function> functions, string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_REPORT_VIEW, functions))
            {
                lstMenu.Add(new MenuItem { Name = "Dân tộc", Url = Url.Action("People", "Report"), ClassCss = "fa fa-caret-right" });
                lstMenu.Add(new MenuItem { Name = "Trang phục", Url = Url.Action("NationalCostume", "Report"), ClassCss = "fa fa-caret-right" });
            }    
            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = "fas fa-chart-line", IsOpen = isOpen };
            }
            return null;
        }

        private MenuItem CreateNewsCategoryMenu(GroupFunction group, List<Function> functions, string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_NEWS_CATEGORY_VIEW, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách", Url = Url.Action("Index", "NewsCategory"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_NEWS_CATEGORY_ADD, functions))
                lstMenu.Add(new MenuItem { Name = "Thêm mới", Url = Url.Action("Create", "NewsCategory"), ClassCss = "fa fa-caret-right" });
            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
                item.IsActive = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = "fa fa-pencil-alt", IsOpen = isOpen , Area="Danh mục" };
            }
            return null;
        }

        private MenuItem CreateNationalCostumeCategoryMenu(GroupFunction group, List<Function> functions, string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_NATIONAL_COSTUME_CATEGORY_VIEW, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách", Url = Url.Action("Index", "NationalCostumeCategory"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_NATIONAL_COSTUME_CATEGORY_ADD, functions))
                lstMenu.Add(new MenuItem { Name = "Thêm mới", Url = Url.Action("Create", "NationalCostumeCategory"), ClassCss = "fa fa-caret-right" });
            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
                item.IsActive = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = "fas fa-magic", IsOpen = isOpen };
            }
            return null;
        }

        private MenuItem CreateNationalCostumeMenu(GroupFunction group, List<Function> functions, string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_NATIONAL_COSTUME_VIEW, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách", Url = Url.Action("Index", "NationalCostume"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_NATIONAL_COSTUME_ADD, functions))
            {
                lstMenu.Add(new MenuItem { Name = "Thêm mới", Url = Url.Action("Create", "NationalCostume"), ClassCss = "fa fa-caret-right" });
                //lstMenu.Add(new MenuItem { Name = "Import Excel", Url = Url.Action("ImportExcel", "NationalCostume"), ClassCss = "fa fa-caret-right" });
            }

            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
                item.IsActive = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = "fas fa-vest-patches", IsOpen = isOpen };
            }
            return null;
        }

        private MenuItem CreatePeopleCategoryMenu(GroupFunction group, List<Function> functions, string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_PEOPLE_CATEGORY_VIEW, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách", Url = Url.Action("Index", "PeopleCategory"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_PEOPLE_CATEGORY_ADD, functions))
                lstMenu.Add(new MenuItem { Name = "Thêm mới", Url = Url.Action("Create", "PeopleCategory"), ClassCss = "fa fa-caret-right" });
            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
                item.IsActive = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = "fas fa-portrait", IsOpen = isOpen };
            }
            return null;
        }

        private MenuItem CreatePeopleMenu(GroupFunction group, List<Function> functions, string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_PEOPLE_VIEW, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách - Chưa gửi duyệt", Url = Url.Action("Index", "People"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_PEOPLE_VIEW_REQUEST, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách - Chờ duyệt", Url = Url.Action("IndexRequest", "People"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_PEOPLE_VIEW_CONFIRM, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách - Đã duyệt", Url = Url.Action("IndexConfirm", "People"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_PEOPLE_VIEW_REJECT, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách - Hủy duyệt", Url = Url.Action("IndexReject", "People"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_PEOPLE_VIEW_DELETE, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách - Đã xóa", Url = Url.Action("IndexDelete", "People"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_PEOPLE_ADD, functions))
            {
                lstMenu.Add(new MenuItem { Name = "Thêm mới", Url = Url.Action("Create", "People"), ClassCss = "fa fa-caret-right" });
                //lstMenu.Add(new MenuItem { Name = "Import Excel", Url = Url.Action("ImportExcel", "People"), ClassCss = "fa fa-caret-right" });
            }    
    
            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
                item.IsActive = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = "fas fa-user-plus", IsOpen = isOpen };
            }
            return null;
        }

        private MenuItem CreateNewsMenu(GroupFunction group, List<Function> functions, string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_NEWS_VIEW, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách", Url = Url.Action("Index", "News"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_NEWS_ADD, functions))
                lstMenu.Add(new MenuItem { Name = "Thêm mới", Url = Url.Action("Create", "News"), ClassCss = "fa fa-caret-right" });
            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
                item.IsActive = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = "fa fa-newspaper", IsOpen = isOpen };
            }
            return null;
        }

        private MenuItem CreateContactMenu(GroupFunction group, List<Function> functions, string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_CONTACT_VIEW, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách", Url = Url.Action("Index", "Contact"), ClassCss = "fa fa-caret-right" });
            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = "fas fa-address-book", IsOpen = isOpen };
            }
            return null;
        }

        private MenuItem CreateSettingMenu(GroupFunction group, List<Function> functions, string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_SETTING, functions))
                lstMenu.Add(new MenuItem { Name = "Cấu hình", Url = Url.Action("Setting", "Default"), ClassCss = "fa fa-caret-right" });
            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
                item.IsActive = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = "fa fa-cog", IsOpen = isOpen };
            }
            return null;
        }

        private MenuItem CreateLogMenu(GroupFunction group, List<Function> functions, string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_LOG_VIEW, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách", Url = Url.Action("Index", "Log"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_LOG_SETTING, functions))
                lstMenu.Add(new MenuItem { Name = "Kiểm soát truy cập", Url = Url.Action("Setting", "Log"), ClassCss = "fa fa-caret-right" });
            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
                item.IsActive = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = "fa fa-desktop", IsOpen = isOpen };
            }
            return null;
        }

        private MenuItem? CreateCompanyMenu(GroupFunction group, List<Function> functions, string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_COMPANY_VIEW, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách", Url = Url.Action("Index", "Company"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_COMPANY_ADD, functions))
                lstMenu.Add(new MenuItem { Name = "Thêm mới", Url = Url.Action("Create", "Company"), ClassCss = "fa fa-caret-right" });
            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
                item.IsActive = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, IsOpen = isOpen };
            }
            return null;
        }

        private MenuItem? CreateRoleMenu(GroupFunction group, List<Function> functions, string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_ROLE_VIEW, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách", Url = Url.Action("Index", "Role"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_ROLE_ADD, functions))
                lstMenu.Add(new MenuItem { Name = "Thêm mới", Url = Url.Action("Create", "Role"), ClassCss = "fa fa-caret-right" });
            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
                item.IsActive = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = "fa fa-sitemap", IsOpen = isOpen };
            }
            return null;
        }

        private MenuItem? CreateUserMenu(GroupFunction group, List<Function> functions, string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_USER_VIEW, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách", Url = Url.Action("Index", "User"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_USER_ADD, functions))
                lstMenu.Add(new MenuItem { Name = "Thêm mới", Url = Url.Action("Create", "User"), ClassCss = "fa fa-caret-right" });
            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
                item.IsActive = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = "fa fa-users", IsOpen = isOpen };
            }
            return null;
        }

        private MenuItem? CreatePositionMenu(GroupFunction group, List<Function> functions, string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_POSITION_VIEW, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách", Url = Url.Action("Index", "Position"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_POSITION_ADD, functions))
                lstMenu.Add(new MenuItem { Name = "Thêm mới", Url = Url.Action("Create", "Position"), ClassCss = "fa fa-caret-right" });
            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
                item.IsActive = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = "fa fa-briefcase", IsOpen = isOpen , Area="Quản trị" };
            }
            return null;
        }

        private MenuItem? CreateDepartmentMenu(GroupFunction group, List<Function> functions, string url)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(Constants.PERMISSION_DEPARTMENT_VIEW, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách", Url = Url.Action("Index", "Department"), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(Constants.PERMISSION_DEPARTMENT_ADD, functions))
                lstMenu.Add(new MenuItem { Name = "Thêm mới", Url = Url.Action("Create", "Department"), ClassCss = "fa fa-caret-right" });
            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
                item.IsActive = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = "fa fa-building", IsOpen = isOpen };
            }
            return null;
        }

        /// <summary>
        /// Common function to create menu item
        /// </summary>
        /// <param name="group"></param>
        /// <param name="functions"></param>
        /// <param name="controllerName"></param>
        /// <param name="url"></param>
        /// <param name="viewPermission"></param>
        /// <param name="addPermission"></param>
        /// <param name="cssClass"></param>
        /// <returns></returns>
        private MenuItem CreateMenuItem(string controllerName, GroupFunction group, List<Function> functions, string url, string viewPermission, string addPermission, string cssClass)
        {
            var lstMenu = new List<MenuItem>();
            if (IsPermisson(viewPermission, functions))
                lstMenu.Add(new MenuItem { Name = "Danh sách", Url = Url.Action("Index", controllerName), ClassCss = "fa fa-caret-right" });
            if (IsPermisson(addPermission, functions))
                lstMenu.Add(new MenuItem { Name = "Thêm mới", Url = Url.Action("Create", controllerName), ClassCss = "fa fa-caret-right" });

            bool isOpen = false;
            foreach (var item in lstMenu.Where(o => o.Url.Equals(url)))
            {
                isOpen = true;
                item.IsOpen = true;
            }
            if (lstMenu.Any())
            {
                return new MenuItem { Name = group.Name, Childrens = lstMenu, ClassCss = cssClass, IsOpen = isOpen };
            }
            return null;
        }

        /// <summary>
        /// For this scope all additional scope will have the same permission with WARD function
        /// </summary>
        /// <param name="group"></param>
        /// <param name="functions"></param>
        /// <param name="url"></param>
        /// <returns></returns>


        private bool IsPermisson(string permissionKey, List<Function> functions)
        {
            return functions.Any(o => o.FunctionCode.Equals(permissionKey));
            //return true;
        }

        [OutputCache(PolicyName = "10Minute")]
        internal bool CheckFunctionPermission(string permissionKey)
        {
            var result = false;
            if (UserData.SuperAdmin)
            {
                return true;
            }
            var permissionResult = Authen.GetFunctionsByUserId(UserData.Id);
            if (permissionResult.IsSuccess())
            {
                result = permissionResult.Value.Any(o => o.FunctionCode.Equals(permissionKey));
            }
            return result;
        }

        internal int CheckCompanyUser(int? companyId = null)
        {
            var companyidCurent = 0;
            UserInfo? userInfo;
            userInfo = Authen.GetSessionUserInfo();
            companyidCurent = Authen.GetUserCompanyId(userInfo.Id);
            if (companyId.HasValue)
            {
                var arrayCompany = CreateArrayCompany();
                if (arrayCompany.Contains(companyId.Value))
                {
                    companyidCurent = companyId.Value;
                }
            }
            return companyidCurent;
        }

        private List<int> CreateArrayCompany()
        {
            UserInfo? userInfo;
            var ArrayCompany = new List<int>();
            var AllCompany = Authen.GetAllCompanyId();

            userInfo = Authen.GetSessionUserInfo();
            if (userInfo != null)
            {
                var company = Authen.GetUserCompany(userInfo.Id);
                if (company != null)
                {
                    ArrayCompany.Add(company.Id);
                    if (company.Childrens != null)
                    {
                        foreach (var item in company.Childrens)
                        {
                            if (item.Status == Enums.ActiveStatus.Active)
                            {
                                ArrayCompany.AddRange(CreateArrayCompany(item));
                            }
                        }
                    }
                }
            }
            return ArrayCompany;
        }

        private List<int> CreateArrayCompany(Company company, bool? viewAll = true)
        {
            var listCompany = new List<int>();
            if (company != null)
            {
                listCompany.Add(company.Id);
                if (viewAll.Value)
                {
                    if (company.Childrens != null)
                    {
                        foreach (var item in company.Childrens)
                        {
                            if (item.Status == Enums.ActiveStatus.Active)
                            {
                                listCompany.AddRange(CreateArrayCompany(item));
                            }
                        }
                    }
                }
            }
            return listCompany;
        }


        internal void SetError(MessageResult message, Exception ex)
        {
            message.Code = Enums.ErrorCode.Error;
            message.Message = ex.Message;
            Log.LogError(ex, ex.Message);
        }
        internal void SetValidateMessage(MessageResult message, ModelStateDictionary state)
        {
            message.Code = Enums.ErrorCode.Validate;
            message.Message = string.Join(", ", state.Values.SelectMany(x => x.Errors.Select(t => t.ErrorMessage)).ToList());

        }
    }
}
