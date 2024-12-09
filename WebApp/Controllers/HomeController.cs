using Common;
using Datas.Entity;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services;
using System.Diagnostics;
using WebApp.Common;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PeopleCategoryService _peopleCategoryService;
        private readonly SettingUtils _settingUtils;
        private readonly NationalCostumeService _nationalCostumeService;
        private readonly NewsService _newsService;
        private readonly PeopleService _peopleService;
        public HomeController(ILogger<HomeController> logger, SettingUtils settingUtils, PeopleCategoryService peopleCategoryService, NationalCostumeService nationalCostumeService, NewsService newsService, PeopleService peopleService) : base(peopleCategoryService, settingUtils)
        {
            _logger = logger;
            _settingUtils = settingUtils;
            _peopleCategoryService = peopleCategoryService;
            _nationalCostumeService = nationalCostumeService;
            _newsService = newsService;
            _peopleService = peopleService;
        }

        public IActionResult Index()
        {
            //GetListPeopleCategory();
            GetListPeople();
            GetNationalCostume();
            //GetListNews();
            GetTopNews();
            GetMap();
            return View();
        }

        private void GetListPeopleCategory()
        {
            var data = _peopleCategoryService.GetAll(o => o.Status == Enums.ActiveStatus.Active && !o.ParentId.HasValue).Select(o => o.ToPeopleCategoryModel()).ToList();
            ViewBag.PeopleCategories = data;
        }

        private void GetListPeople()
        {
            var data = _peopleService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.ConfirmStatus == Enums.PeopleConfirmStatus.Confirm && !o.ParentId.HasValue && o.IsDisplay).ToList().Select(o => o.ToPeopleModel()).ToList();
            ViewBag.Peoples = data;
        }

        private void GetNationalCostume()
        {
            var lstData = new List<NationalCostumeModel>();
            var data = _nationalCostumeService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.DeleteStatus == Enums.DeleteStatus.Normal && o.IsDisplay).OrderByDescending(o => o.Top).ThenByDescending(o => o.UpdateDate);
            foreach (var item in data.Take(3))
            {
                lstData.Add(item.ToNationalCostumeModel());
            }
            ViewBag.NationalCostumes = lstData;
        }

        private void GetMap()
        {
            var lstData = _peopleService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.ConfirmStatus == Enums.PeopleConfirmStatus.Confirm && o.DeleteStatus == Enums.DeleteStatus.Normal && o.IsDisplay).OrderByDescending(o => o.Top).ThenByDescending(o => o.UpdateDate);
            var lstMap = new List<object>();
            foreach (var item in lstData)
            {
                var locations = item.Locations.Where(o => o.DeleteStatus == Enums.DeleteStatus.Normal);
                foreach (var location in locations)
                {
                    if (!string.IsNullOrEmpty(location.Lat) && !string.IsNullOrEmpty(location.Long))
                    {
                        var strData = string.Empty;
                        if(string.IsNullOrEmpty(Settings.SubDomain))
                        {
                            strData += "<img src=" + item.Image0 + " style=width:200px;height:156px;  ><br/><a style=text-decoration:none; href=/dan-toc/chi-tiet/" + item.Url + ">" + item.Name + "</a>";
                            if (!string.IsNullOrEmpty(item.Address))
                                strData += "<br/>Địa chỉ: " + item.Address + "";
                            lstMap.Add(new List<object> {
                            strData,
                            location.Lat,
                            location.Long,
                            "/fe/images/icon2.png",
                            item.ParentId.HasValue ? item.ParentId: item.Id
                        });
                        }    
                        else
                        {
                            strData += "<img src=" + item.Image0 + " style=width:200px;height:156px;  ><br/><a style=text-decoration:none; href=/ethnic-minority/details/" + item.Url + ">" + item.Name + "</a>";
                            if (!string.IsNullOrEmpty(item.Address))
                                strData += "<br/>Address: " + item.Address + "";
                            lstMap.Add(new List<object> {
                            strData,
                            location.Lat,
                            location.Long,
                            "/fe/images/icon2.png",
                            item.ParentId.HasValue ? item.ParentId: item.Id
                        });
                        }    
                    }
                }
                
            }
            //var lstMapType = _peopleCategoryService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.DeleteStatus == Enums.DeleteStatus.Normal).Select(o => o.ToHistoricalRelicTypeModel()).ToList();

            ViewBag.ListMap = JsonConvert.SerializeObject(lstMap);
            //ViewBag.ListMapType = lstMapType;
        }

        private void GetTopNews()
        {
            var data = _newsService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.PostDate <= DateTime.Now && o.DeleteStatus == Enums.DeleteStatus.Normal && o.PostDate <= DateTime.Now).OrderByDescending(o => o.Top).ThenByDescending(o => o.UpdateDate).Take(5).ToList().Select(o=>o.ToNewsModel()).ToList();
            ViewBag.TopNews = data;
        }

        private void GetListNews()
        {
            var lstData = new List<NewsInfo>();
            for (int i = 1; i <= 12; i++)
            {
                var newsInfo = new NewsInfo { Month = i };
                var data = GetNews(i);
                if (data != null)
                {
                    newsInfo.isDisplay = true;
                    newsInfo.Title = data.Title;
                    newsInfo.PostDate = data.PostDate;
                    newsInfo.Url = data.Url;
                    newsInfo.Calendar = data.Calendar;
                    newsInfo.Image = data.Image;
                    newsInfo.Details = data.Details;
                }
                lstData.Add(newsInfo);
            }
            ViewBag.News = lstData;
        }

        private NewsModel GetNews(int month)
        {
            var data = _newsService.GetAll(o => o.Status == Enums.ActiveStatus.Active && o.PostDate <= DateTime.Now && o.DeleteStatus == Enums.DeleteStatus.Normal && o.PostDate.Month == month).OrderByDescending(o => o.Top).ThenByDescending(o => o.UpdateDate).FirstOrDefault();
            if (data != null)
                return data.ToNewsModel();
            return null;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
