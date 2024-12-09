using Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services;
using WebApp.Common;

namespace WebApp.Controllers
{
    [Route("ban-do")]
    [Route("map")]
    public class FEMapController : BaseController
    {

        private readonly PeopleCategoryService _peopleCategoryService;
        private readonly PeopleService _peopleService;
        public FEMapController(PeopleCategoryService peopleCategoryService, SettingUtils settingUtils, PeopleService peopleService) : base(peopleCategoryService, settingUtils)
        {
            _peopleCategoryService = peopleCategoryService;
            _peopleService = peopleService;
        }

        [Route("")]
        public IActionResult Index()
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
                        if (string.IsNullOrEmpty(Settings.SubDomain))
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
            var lstMapType = _peopleService.GetAll(o => o.Status == Enums.ActiveStatus.Active && !o.ParentId.HasValue).ToList().Select(o => o.ToPeopleModel()).ToList();

            ViewBag.ListMap = JsonConvert.SerializeObject(lstMap);
            ViewBag.ListMapType = lstMapType;
            return View();
        }
    }
}
