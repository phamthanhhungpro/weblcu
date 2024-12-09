using Common;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services;
using WebApp.Common;

namespace WebApp.Controllers
{
    [Route("danh-muc-dan-toc")]
    [Route("list-ethnic-minority")]
    public class FEPeopleCategoryController : BaseController
    {
        private readonly PeopleCategoryService _peopleCategoryService;
        public FEPeopleCategoryController(PeopleCategoryService peopleCategoryService, SettingUtils settingUtils) : base(peopleCategoryService, settingUtils)
        {
             _peopleCategoryService = peopleCategoryService;
        }

        [Route("{name}/{page?}")]
        [Route("tim-kiem/{name}/{search}/{page?}")]
        [Route("search/{name}/{search}/{page?}")]
        public ActionResult Details(string name, int? page, string search)
        {
            var data = _peopleCategoryService.GetFirstOrDefault(o => !string.IsNullOrEmpty(o.Url) && o.Url.Equals(name) && o.Status == Enums.ActiveStatus.Active, "Peoples");
            if (data != null)
            {
                GetPeopleModel(data, page, search);
                return View(data.ToPeopleCategoryModel());
            }
            //base.ErrorNotification("Tài khoản không được cấp quyền này");
            return RedirectToAction("Index", "Home");
        }

        private void GetPeopleModel(PeopleCategory peopleCategory, int? page, string search)
        {
            var lstData = new List<PeopleModel>();
            if (!page.HasValue)
                page = 1;
            IEnumerable<People> data = null;
            bool isSearch = false;
            if (peopleCategory != null && peopleCategory.Peoples !=null)
            {
                if (!string.IsNullOrEmpty(search))
                {
                    isSearch = true;
                    ViewBag.SearchValue = search;
                    search = search.ToLower();
                    data = peopleCategory.Peoples.Where(o => o.Name.ToLower().Contains(search) && o.Status == Enums.ActiveStatus.Active && o.ConfirmStatus == Enums.PeopleConfirmStatus.Confirm && o.DeleteStatus == Enums.DeleteStatus.Normal && o.IsDisplay).OrderByDescending(o => o.Top).ThenByDescending(o => o.UpdateDate);
                }
                else

                    data = peopleCategory.Peoples.Where(o => o.Status == Enums.ActiveStatus.Active && o.ConfirmStatus == Enums.PeopleConfirmStatus.Confirm && o.DeleteStatus == Enums.DeleteStatus.Normal && o.IsDisplay).OrderByDescending(o => o.Top).ThenByDescending(o => o.UpdateDate);
                var total = data.Count();
                //var page = total / 9;
                var start = page.HasValue ? (page.Value <= 1 ? 0 : ((page - 1) * 9)) : 0;
                foreach (var item in data.Skip((int)start).Take(9))
                {
                    lstData.Add(item.ToPeopleModel());
                }
                ViewBag.CurrentPage = page;
                ViewBag.MaxPage = Math.Ceiling((double)total / 9);
            }
            else
            {
                ViewBag.CurrentPage = page;
                ViewBag.MaxPage = 1;
            }
            ViewBag.IsSearch = isSearch;
            ViewBag.Peoples = lstData;
        }
    }
}
