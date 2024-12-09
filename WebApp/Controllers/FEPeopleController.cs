using Common;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services;
using WebApp.Common;

namespace WebApp.Controllers
{
    [Route("dan-toc")]
    [Route("ethnic-minority")]
    public class FEPeopleController : BaseController
    {
        private PeopleService _peopleService;
        public FEPeopleController(PeopleCategoryService peopleCategoryService, PeopleService peopleService, SettingUtils settingUtils) : base(peopleCategoryService, settingUtils)
        {
            _peopleService=peopleService;
        }

        [Route("{page?}")]
        [Route("tim-kiem/{search}/{page?}")]
        [Route("search/{search}/{page?}")]
        public ActionResult Index(int? page, string search)
        {
            var lstData = new List<PeopleModel>();
            if (!page.HasValue)
                page = 1;
            IEnumerable<People> data = null;
            bool isSearch = false;
            if (!string.IsNullOrEmpty(search))
            {
                isSearch = true;
                ViewBag.SearchValue = search;
                search = search.ToLower();
                data = _peopleService.GetAll(o => o.Name.ToLower().Contains(search) && o.Status == Enums.ActiveStatus.Active && o.ConfirmStatus == Enums.PeopleConfirmStatus.Confirm && o.IsDisplay && !o.ParentId.HasValue).OrderByDescending(o => o.Top).ThenByDescending(o => o.UpdateDate);
            }
            else
                data = _peopleService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.ConfirmStatus == Enums.PeopleConfirmStatus.Confirm && o.DeleteStatus == Enums.DeleteStatus.Normal && o.IsDisplay && !o.ParentId.HasValue).OrderByDescending(o => o.Top).ThenByDescending(o => o.UpdateDate);
            var total = data.Count();
            //var page = total / 9;
            var start = page.HasValue ? (page.Value <= 1 ? 0 : ((page - 1) * 9)) : 0;
            foreach (var item in data.Skip((int)start).Take(9))
            {
                lstData.Add(item.ToPeopleModel());
            }
            ViewBag.CurrentPage = page;
            ViewBag.MaxPage = Math.Ceiling((double)total / 9);
            ViewBag.IsSearch = isSearch;
            ViewBag.Peoples = lstData;
            return View();
        }


        [Route("chi-tiet/{name}")]
        [Route("details/{name}")]
        public ActionResult Details(string name)
        {
            var data = _peopleService.GetFirstOrDefault(o => o.Url.Equals(name) && o.Status == Enums.ActiveStatus.Active, "NationalCostumes", "PeopleCategory");
            if (data != null)
            {
                _peopleService.AddView(data.Id);
                ViewBag.NationalCostumes = GetNationalCostumeModel(data);
                ViewBag.OtherPeoples = GetOtherPeoples(data);
                //ViewBag.OtherPeopleCategories = GetOtherPeopleCategory(data);
                ViewBag.Host = Request.Scheme + "://" + Request.Host; //Request.Url.Scheme + "://" + Request.Url.Authority;
                return View(data.ToPeopleModel());
            }
            //base.ErrorNotification("Tài khoản không được cấp quyền này");
            return RedirectToAction("Index", "Home");
        }

        private List<NationalCostumeModel> GetNationalCostumeModel(People people)
        {
            var data = new List<NationalCostumeModel>();
            if (people != null)
            {
                foreach (var item in people.NationalCostumes.Where(o => o.Status == Enums.ActiveStatus.Active && o.IsDisplay == true && o.DeleteStatus == Enums.DeleteStatus.Normal))
                {
                    data.Add(item.ToNationalCostumeModel());
                }
            }
            return data;
        }

        private List<PeopleModel> GetOtherPeoples(People people)
        {
            var data = new List<PeopleModel>();
            var peoples = _peopleService.GetAll(o => o.Id != people.Id && o.Status == Enums.ActiveStatus.Active && o.ConfirmStatus == Enums.PeopleConfirmStatus.Confirm && o.IsDisplay == true && o.DeleteStatus == Enums.DeleteStatus.Normal && !o.ParentId.HasValue).ToList();
            foreach (var item in peoples)
            {
                data.Add(item.ToPeopleModel());
            }
            return data;
        }

        private List<PeopleCategoryModel> GetOtherPeopleCategory(People people)
        {
            var data = new List<PeopleCategoryModel>();
            if (people != null && people.PeopleCategory != null && people.PeopleCategory.Childrens != null)
            {
                foreach (var item in people.PeopleCategory.Childrens.Where(o => o.Id != people.Id && o.Status == Enums.ActiveStatus.Active  && o.DeleteStatus == Enums.DeleteStatus.Normal))
                {
                    data.Add(item.ToPeopleCategoryModel());
                }
            }
            return data;
        }
    }
}
