using Common;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services;
using WebApp.Common;

namespace WebApp.Controllers
{
    [Route("nhac-cu")]
    [Route("instrument")]
    public class FEInstrumentController : BaseController
    {
        private InstrumentService _InstrumentService;
        public FEInstrumentController(InstrumentService InstrumentService, PeopleCategoryService peopleCategoryService, SettingUtils settingUtils) : base(peopleCategoryService, settingUtils)
        {
            _InstrumentService = InstrumentService;
        }
        // GET: News
        [Route("{page?}")]
        [Route("tim-kiem/{search}/{page?}")]
        [Route("search/{search}/{page?}")]
        public ActionResult Index(int? page, string search)
        {
            GetData(page, search);
            return View();
        }

        [Route("chi-tiet/{name}")]
        [Route("details/{name}")]
        public ActionResult Details(string name)
        {
            var data = _InstrumentService.GetFirstOrDefault(o => o.Url.Equals(name) && o.Status == Enums.ActiveStatus.Active, "Category");
            if (data != null)
            {
                _InstrumentService.AddView(data.Id);
                ViewBag.OtherInstrument = GetOtherInstrument(data);
                ViewBag.Host = Request.Scheme + "://" + Request.Host; //Request.Url.Scheme + "://" + Request.Url.Authority;
                //GetTopNews(data.Id);
                return View(data.ToModel());
            }
            //base.ErrorNotification("Tài khoản không được cấp quyền này");
            return RedirectToAction("Index", "Home");
        }

        private List<InstrumentModel> GetOtherInstrument(Instrument national)
        {
            var data = new List<InstrumentModel>();
            if (national != null && national.Category != null && national.Category.Instruments != null)
            {
                foreach (var item in national.Category.Instruments.Where(o => o.Id != national.Id && o.Status == Enums.ActiveStatus.Active && o.DeleteStatus == Enums.DeleteStatus.Normal && o.IsDisplay).OrderByDescending(o => o.Top).ThenByDescending(o => o.UpdateDate).Take(100))
                {
                    data.Add(item.ToModel());
                }
            }
            return data;
        }

            //private void GetTopNews(int id)
            //{
            //    var lstData = new List<NewsModel>();
            //    var data = _newsService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.DeleteStatus == Enums.DeleteStatus.Normal && !o.Id.Equals(id)).OrderByDescending(o => o.Top).ThenByDescending(o => o.UpdateDate);
            //    foreach (var item in data.Take(3))
            //    {
            //        lstData.Add(item.ToNewsModel());
            //    }
            //    ViewBag.lstTopNews = lstData;
            //}

            private void GetData(int? page, string search)
        {
            IEnumerable<Instrument> data = null;
            var lstData = new List<InstrumentModel>();
            if (!page.HasValue)
                page = 1;
            bool isSearch = false;
            if (!string.IsNullOrEmpty(search))
            {
                isSearch = true;
                ViewBag.SearchValue = search;
                search = search.ToLower();
                data = _InstrumentService.GetAll(o => (o.Name.ToLower().Contains(search) || (!string.IsNullOrEmpty(o.Shape) && o.Shape.ToLower().Contains(search)) || (!string.IsNullOrEmpty(o.CurrentStatus) && o.CurrentStatus.ToLower().Contains(search)) || (!string.IsNullOrEmpty(o.Technique) && o.Technique.ToLower().Contains(search)) || (!string.IsNullOrEmpty(o.Classify) && o.Classify.ToLower().Contains(search)) || (!string.IsNullOrEmpty(o.Certification) && o.Certification.ToLower().Contains(search)) || (!string.IsNullOrEmpty(o.Material) && o.Material.ToLower().Contains(search)) || (!string.IsNullOrEmpty(o.Color) && o.Color.ToLower().Contains(search))) && o.Status == Enums.ActiveStatus.Active && o.IsDisplay).OrderByDescending(o => o.Top).ThenByDescending(o => o.UpdateDate);
            }
            else
                data = _InstrumentService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.IsDisplay).OrderByDescending(o => o.Top).ThenByDescending(o => o.UpdateDate);

            var total = data.Count();
            var start = page.HasValue ? (page.Value <= 1 ? 0 : ((page - 1) * 9)) : 0;
            foreach (var item in data.Skip((int)start).Take(9))
            {
                lstData.Add(item.ToModel());
            }
            ViewBag.IsSearch = isSearch;
            ViewBag.CurrentPage = page;
            ViewBag.MaxPage = Math.Ceiling((double)total / 9);
            ViewBag.Instruments = lstData;
        }
    }
}
