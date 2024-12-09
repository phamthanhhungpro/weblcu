using Common;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services;
using WebApp.Common;

namespace WebApp.Controllers
{
    [Route("tin-tuc")]
    [Route("news")]
    public class FENewsController : BaseController
    {
        private NewsService _newsService;
        public FENewsController(NewsService newsService, PeopleCategoryService peopleCategoryService, SettingUtils settingUtils) : base(peopleCategoryService, settingUtils)
        {
            _newsService = newsService;
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
            var data = _newsService.GetFirstOrDefault(o => o.Url.Equals(name) && o.Status == Enums.ActiveStatus.Active, "Category");
            if (data != null)
            {
                _newsService.AddView(data.Id);
                ViewBag.OtherNews = GetOtherNews(data);
                GetTopNews(data.Id);
                return View(data.ToNewsModel());
            }
            //base.ErrorNotification("Tài khoản không được cấp quyền này");
            return RedirectToAction("Index", "Home");
        }

        private List<NewsModel> GetOtherNews(News news)
        {
            var data = new List<NewsModel>();
            if (news != null && news.Category != null && news.Category.News!=null)
            {
                foreach (var item in news.Category.News.Where(o => o.Id != news.Id && o.Status == Enums.ActiveStatus.Active && o.PostDate <= DateTime.Now && o.DeleteStatus == Enums.DeleteStatus.Normal).OrderByDescending(o => o.Top).ThenByDescending(o => o.UpdateDate).ThenByDescending(o => o.PostDate).Take(100))
                {
                    data.Add(item.ToNewsModel());
                }
            }
            return data;
        }

        private void GetTopNews(int id)
        {
            var lstData = new List<NewsModel>();
            var data = _newsService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.DeleteStatus == Enums.DeleteStatus.Normal && !o.Id.Equals(id)).OrderByDescending(o => o.Top).ThenByDescending(o => o.UpdateDate);
            foreach (var item in data.Take(3))
            {
                lstData.Add(item.ToNewsModel());
            }
            ViewBag.TopNews = lstData;
        }

        private void GetData(int? page, string search)
        {
            IEnumerable<News> data = null;
            var lstData = new List<NewsModel>();
            if (!page.HasValue)
                page = 1;
            bool isSearch = false;
            if (!string.IsNullOrEmpty(search))
            {
                isSearch = true;
                ViewBag.SearchValue = search;
                search = search.ToLower();
                data = _newsService.GetAll(o => (o.Title.ToLower().Contains(search) || (!string.IsNullOrEmpty(o.Author) && o.Author.ToLower().Contains(search)) || (!string.IsNullOrEmpty(o.KeyWord) && o.KeyWord.ToLower().Contains(search))) && o.Status == Enums.ActiveStatus.Active && o.PostDate <= DateTime.Now).OrderByDescending(o => o.Top).ThenByDescending(o => o.UpdateDate).ThenByDescending(o => o.PostDate);
            }
            else
                data = _newsService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.PostDate <= DateTime.Now).OrderByDescending(o => o.Top).ThenByDescending(o => o.UpdateDate).ThenByDescending(o => o.PostDate);

            var total = data.Count();
            var start = page.HasValue ? (page.Value <= 1 ? 0 : ((page - 1) * 9)) : 0;
            foreach (var item in data.Skip((int)start).Take(9))
            {
                lstData.Add(item.ToNewsModel());
            }
            ViewBag.IsSearch = isSearch;
            ViewBag.CurrentPage = page;
            ViewBag.MaxPage = Math.Ceiling((double)total / 9);
            ViewBag.News = lstData;
        }
    }
}
