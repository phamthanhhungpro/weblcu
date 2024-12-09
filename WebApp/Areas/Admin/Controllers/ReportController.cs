using Common;
using Common.Entity;
using Common.Entity.Permission;
using Datas.Models.DomainModels;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using System.Linq.Expressions;
using WebApp.Common;

namespace WebApp.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Route("[area]/[controller]")]
    public class ReportController(LogService logService, ILogger<ReportController> logger, AuthenUtils authenUtils, PeopleService peopleService, PeopleCategoryService peopleCategoryService, WardService wardService, DistrictService districtService, NationalCostumeService nationalCostumeService, NationalCostumeCategoryService nationalCostumeCategoryService, IWebHostEnvironment environment) : BaseController<ReportController>(logger, authenUtils,logService)
    {
        private IWebHostEnvironment _hostEnvironment = environment;
        private NationalCostumeCategoryService _nationalCostumeCategoryService = nationalCostumeCategoryService;
        private PeopleCategoryService _peopleCategoryService = peopleCategoryService;
        private NationalCostumeService _nationalCostumeService = nationalCostumeService;
        private PeopleService _peopleService = peopleService;
        private WardService _wardService = wardService;
        private DistrictService _districtService = districtService;

        private void GetListDistrict()
        {
            var lstData = new List<SelectListItem>();
            foreach (var item in _districtService.GetAll(x => x.Status == Enums.ActiveStatus.Active))
            {
                lstData.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
            }
            ViewBag.Districts = lstData;
        }

        private void GetListWard(int? dis = null)
        {
            var lstData = new List<SelectListItem>();
            if (dis.HasValue)
            {
                foreach (var item in _wardService.GetAll(x => x.Status == Enums.ActiveStatus.Active && x.DistrictId.HasValue && x.DistrictId == dis))
                {
                    lstData.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
                }
            }
            ViewBag.Wards = lstData;
        }

        private void GetListPeoples(bool getChild = false)
        {
            var lstData = new List<SelectListItem>();
            foreach (var item in _peopleService.GetAll(x => x.Status == Enums.ActiveStatus.Active && x.Parent == null).ToList())
            {
                lstData.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
                if(getChild && item.Childrens!=null)
                {
                    foreach (var child in item.Childrens.Where(x => x.Status == Enums.ActiveStatus.Active && x.DeleteStatus == Enums.DeleteStatus.Normal))
                    {
                        lstData.Add(new SelectListItem { Value = child.Id.ToString(), Text = "--- " + child.Name });
                    }
                }    
            }
            ViewBag.Peoples = lstData;
        }

        private List<SelectListItem> GetListNationalCostumeCategory(int? nationalCostumeCategoryId = null)
        {
            Expression<Func<NationalCostumeCategory, bool>> query = x => x.Status == Enums.ActiveStatus.Active && x.Parent == null;
            if (nationalCostumeCategoryId.HasValue)
            {
                query = ExtensionMethod.AndAlso(query, x => x.Id != nationalCostumeCategoryId.Value);
            }
            var companyData = new List<SelectListItem>();
            foreach (var item in _nationalCostumeCategoryService.GetAll().Where(query.Compile()))
            {
                companyData.AddRange(CreateNationalCostumeCategory(item, nationalCostumeCategoryId));
            }

            return companyData;
        }

        private List<SelectListItem> CreateNationalCostumeCategory(NationalCostumeCategory nationalCostumeCategory, int? newsCategoryId, string prefix = "")
        {
            var lstData = new List<SelectListItem>();
            lstData.Add(new SelectListItem { Value = nationalCostumeCategory.Id.ToString(), Text = prefix + nationalCostumeCategory.Name });
            foreach (var item in nationalCostumeCategory.Childrens)
            {
                if (newsCategoryId.HasValue && !item.Id.Equals(newsCategoryId.Value))
                {
                    lstData.AddRange(CreateNationalCostumeCategory(item, newsCategoryId, prefix + "---"));
                }
                else if (!newsCategoryId.HasValue)
                {
                    lstData.AddRange(CreateNationalCostumeCategory(item, newsCategoryId, prefix + "---"));
                }
            }
            return lstData;
        }

        private Object BindDataPeople(IEnumerable<People> list)
        {
            var result = new List<object>();
            var index =  1;

            foreach (var item in list)
            {
                result.Add(new
                {
                    item.Id,
                    Index = index,
                    Image = item.Image0,
                    Name = item.Name,
                    Population = item.Population,
                    Address = item.Address,
                    Status = item.Status
                });
                index++;
            }
            return result;
        }

        [HttpPost]
        [Route("GetWardSearch")]
        public ActionResult GetWardSearch(int? id)
        {
            var message = new MessageResult();
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_EDIT))
            {
                GetListWard(id);
                return PartialView("_DisplayWardSearch");
            }
            else
            {
                message.Code = Enums.ErrorCode.OtherNotPermisson;
                Response.StatusCode = 500;
            }
            return Json(message);
        }

        [Route("People")]
        public IActionResult People()
        {
            if (CheckFunctionPermission(Constants.PERMISSION_REPORT_VIEW))
            {
                GetListDistrict();
                GetListWard();
                GetListPeoples();
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        [HttpPost]
        [Route("GetPeopleSearch")]
        public ActionResult GetPeopleSearch(string category, string district, string ward, string people, string display)
        {
            MessageResult message = new MessageResult();
            IEnumerable<People> list = null;
            

            var c = 0;
            var p = 0;
            var d = 0;
            var w = 0;
            var dis = 0;
            if (!string.IsNullOrEmpty(category))
                int.TryParse(category, out c);
            if (!string.IsNullOrEmpty(people))
                int.TryParse(people, out p);
            if (!string.IsNullOrEmpty(district))
                int.TryParse(district, out d);
            if (!string.IsNullOrEmpty(ward))
                int.TryParse(ward, out w);
            if (!string.IsNullOrEmpty(display))
                int.TryParse(display, out dis);
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW))
            {
                Expression<Func<People, bool>> expression = o => o.DeleteStatus == Enums.DeleteStatus.Normal;
                if (c > 0)
                {
                    expression = expression.AndAlso1(o => o.PeopleCategoryId.HasValue && o.PeopleCategoryId == c);
                }
                if (d > 0)
                {
                    expression = expression.AndAlso1(o => o.Locations.Any(x => x.DistrictId.HasValue && x.DistrictId == d));
                }
                if (w > 0)
                {
                    expression = expression.AndAlso1(o => o.Locations.Any(x => x.WardId.HasValue && x.WardId == w));
                }
                if (p > 0)
                {
                    expression = expression.AndAlso1(o => o.ParentId.HasValue && o.ParentId == p);
                }
                if (dis ==1 )
                {
                    expression = expression.AndAlso1(o => o.IsDisplay);
                }
                else if (dis == 2)
                {
                    expression = expression.AndAlso1(o => !o.IsDisplay);
                }
                list = _peopleService.GetAll(expression);
            }
            return PartialView("_RenderPeople", list != null ? list.ToList() : new List<People>());
        }

        [Route("NationalCostume")]
        public IActionResult NationalCostume()
        {
            if (CheckFunctionPermission(Constants.PERMISSION_REPORT_VIEW))
            {
                GetListPeoples(true);
                ViewBag.NationalCostumeCategories = GetListNationalCostumeCategory();
                return View();
            }
            else
            {
                base.ErrorNotification("Tài khoản không được cấp quyền này");
                return RedirectToAction("Index", "Default");
            }
        }

        [HttpPost]
        [Route("GetNationalCostumeSearch")]
        public ActionResult GetNationalCostumeSearch(string category, string people, string display, bool sub)
        {
            MessageResult message = new MessageResult();
            IEnumerable<NationalCostume> list = null;


            var p = 0;
            var dis = 0;
            var c = 0;
            if (!string.IsNullOrEmpty(category))
                int.TryParse(category, out c);
            if (!string.IsNullOrEmpty(people))
                int.TryParse(people, out p);
            if (!string.IsNullOrEmpty(display))
                int.TryParse(display, out dis);
            if (CheckFunctionPermission(Constants.PERMISSION_PEOPLE_VIEW))
            {
                Expression<Func<NationalCostume, bool>> expression = o => o.DeleteStatus == Enums.DeleteStatus.Normal;
                if (p > 0)
                {
                    var lstId = new List<int>{ p };
                    if (sub)
                    {
                        var lsPeo = _peopleService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.ParentId.HasValue && o.ParentId == p).Select(o=>o.Id).ToList();
                        if(lsPeo!=null && lsPeo.Count >0)
                        {
                            lstId.AddRange(lsPeo);
                        }    

                    }
                    //if(lstId.Count ==1)
                    //{
                    //    expression = expression.AndAlso1(o => o.PeopleId.HasValue && o.PeopleId == p);
                    //}
                    ////Expression<Func<NationalCostume, bool>> expression1 = o => o.DeleteStatus == Enums.DeleteStatus.Normal;
                    HashSet<int> hash = new HashSet<int>(lstId);
                    expression = expression.AndAlso1(o => o.PeopleId.HasValue && hash.Contains(o.PeopleId.Value));
 
                }
                if (c > 0)
                {
                    expression = expression.AndAlso1(o => o.CategoryId.HasValue && o.CategoryId == c);
                }
                if (dis == 1)
                {
                    expression = expression.AndAlso1(o => o.IsDisplay);
                }
                else if (dis == 2)
                {
                    expression = expression.AndAlso1(o => !o.IsDisplay);
                }
                list = _nationalCostumeService.GetAll(expression).OrderBy(o=>o.Name).ThenBy(o=>o.PeopleId);
            }
            return PartialView("_RenderNationalCostume", list != null ? list.ToList() : new List<NationalCostume>());
        }
    }
}
