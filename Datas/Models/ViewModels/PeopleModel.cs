using Common;
using Datas.Models.DomainModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enums;

namespace Datas.Models.ViewModels
{
    public class PeopleModel : AbstractData
    {
        private string _peopleCategoryName = string.Empty;
        private string _parent = string.Empty;

        [Required(ErrorMessage = "Bạn cần nhập tên dân tộc")]
        [Display(Name = "Tên dân tộc")]
        public string Name { set; get; }

        [Display(Name = "Hiển thị")]
        public bool IsDisplay { set; get; } = true;

        [Display(Name = "Lên đầu")]
        public bool Top { get; set; } = false;

        [Display(Name = "Ảnh 360")]
        public string? Image360 { set; get; }

        [Display(Name = "Địa chỉ")]
        public string? Address { set; get; }

        [Display(Name = "Dân số")]
        public string? Population { set; get; }

        [Display(Name = "Lịch sử")]
        [DataType(DataType.MultilineText)]
        public string? History { set; get; }


        [Display(Name = "Mô tả ngắn")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        [Display(Name = "Tổng quan chung")]
        [Required(ErrorMessage = "Bạn cần nhập tổng quan chung")]
        public string Content { get; set; }

        [Display(Name = "Không gian bản")]
        public string? Content1 { get; set; } = string.Empty;

        [Display(Name = "Không gian sống")]
        public string? Content2 { get; set; } = string.Empty;

        [Display(Name = "Hoạt động sản xuất trồng trọt")]
        public string? Content3 { get; set; } = string.Empty;

        [Display(Name = "Hoạt động sản xuất chăn nuôi")]
        public string? Content4 { get; set; } = string.Empty;

        [Display(Name = "Ẩm thực")]
        public string? Content5 { get; set; } = string.Empty;

        [Display(Name = "Loại dân tộc")]
        public int? PeopleCategoryId { get; set; }

        [Display(Name = "Trực thuộc dân tộc")]
        public int? ParentId { get; set; }


        [Display(Name = "Ảnh đại diện")]
        public string? Image0 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image1 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image2 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image3 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image4 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image5 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image6 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image7 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image8 { set; get; }

        [Display(Name = "Ảnh")]
        public string? Image9 { set; get; }

        //[Display(Name = "Ngôn ngữ")]
        //public int? LanguageId { get; set; }

        [Display(Name = "Ngôn ngữ")]
        public List<int>? LanguageIds { get; set; } = new List<int>();

        public List<LocationModel> Locations { set; get; } = new List<LocationModel>();
        public List<AttachmentModel>? Attachments { set; get; } = new List<AttachmentModel>();

        [Display(Name = "Trạng thái xuất bản")]
        public PeopleConfirmStatus ConfirmStatus { set; get; } = PeopleConfirmStatus.Update;

        public string? Url { set; get; }

        [Display(Name = "Thông tin duyệt")]
        public int IsConfirm { get; set; } = 0;

        [Display(Name = "Ghi chú")]
        public string? ConfirmData { set; get; } = string.Empty;

        public void SetPeopleCategory(string data)
        {
            _peopleCategoryName = data;
        }

        public string GetPeopleCategory()
        {
            return _peopleCategoryName;
        }

        public void SetParent(string data)
        {
            _parent = data;
        }

        public string GetParent()
        {
            return _parent;
        }

        public List<LocationModel> GetListLocationDisplay()
        {
            return Locations.Where(o => !string.IsNullOrEmpty(o.Lat) && !string.IsNullOrEmpty(o.Long) && o.Status == Enums.ActiveStatus.Active).ToList();
        }

        public LocationModel GetFirstLocationDisplay()
        {
            return Locations.FirstOrDefault(o => !string.IsNullOrEmpty(o.Lat) && !string.IsNullOrEmpty(o.Long) && o.Status == Enums.ActiveStatus.Active);
        }
        public string BuildListMap(string subDomain)
        {
            var lstMap = new List<object>();
            var locations = Locations.Where(o => !string.IsNullOrEmpty(o.Lat) && !string.IsNullOrEmpty(o.Long) && o.Status == Enums.ActiveStatus.Active).ToList();
            foreach (var location in locations)
            {
                if (!string.IsNullOrEmpty(location.Lat) && !string.IsNullOrEmpty(location.Long))
                {
                    var strData = string.Empty;
                    if (string.IsNullOrEmpty(subDomain))
                    {
                        strData += "<img src=" + Image0 + " style=width:200px;height:156px;  ><br/><a style=text-decoration:none; href=/dan-toc/chi-tiet/" + Url + ">" + Name + "</a>";
                        if (!string.IsNullOrEmpty(Address))
                            strData += "<br/>Địa chỉ: " + Address + "";
                        lstMap.Add(new List<object> {
                            strData,
                            location.Lat,
                            location.Long,
                            "/fe/images/icon2.png",
                            ParentId.HasValue ? ParentId: Id
                        });
                    }
                    else
                    {
                        strData += "<img src=" + Image0 + " style=width:200px;height:156px;  ><br/><a style=text-decoration:none; href=/ethnic-minority/details/" + Url + ">" + Name + "</a>";
                        if (!string.IsNullOrEmpty(Address))
                            strData += "<br/>Address: " + Address + "";
                        lstMap.Add(new List<object> {
                            strData,
                            location.Lat,
                            location.Long,
                            "/fe/images/icon2.png",
                            ParentId.HasValue ? ParentId: Id
                        });
                    }
                   
                }
            }
            return JsonConvert.SerializeObject(lstMap);
        }

        public string CaclulerDistance(string lat, string lon)
        {
            double localLat = 0;
            double localLon = 0;
            double distanceLat = 0;
            double distanceLon = 0;
            //try
            //{
            //    if (double.TryParse(lat.Replace(".", ","), out distanceLat) && double.TryParse(lon.Replace(".", ","), out distanceLon) && double.TryParse(Lat.Replace(".", ","), out localLat) && double.TryParse(Long.Replace(".", ","), out localLon))
            //    {
            //        return Math.Round(Utilities.DistanceTo(distanceLat, distanceLon, localLat, localLon), 2) + " km";
            //    }
            //}
            //catch
            //{ }
            return string.Empty;
        }
    }
}
