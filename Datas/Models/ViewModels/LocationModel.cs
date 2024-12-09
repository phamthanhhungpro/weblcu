using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class LocationModel : AbstractData
    {
        private string _districtName = string.Empty;
        private string _wardName = string.Empty;

        [Display(Name = "Vĩ độ (lat)")]
        public string? Lat { set; get; }

        [Display(Name = "Kinh độ (long)")]
        public string? Long { set; get; }

        [Display(Name = "Dân tộc")]
        public int? PeopleId { set; get; }

        [Display(Name = "Thành phố/huyện")]
        public int? DistrictId { get; set; }

        [Display(Name = "Phường/xã")]
        public int? WardId { get; set; }

        public void SetWardName(string data)
        {
            _wardName = data;
        }

        public string GetWardName()
        {
            return _wardName;
        }

        public void SetDistrictName(string data)
        {
            _districtName = data;
        }

        public string GetDistrictName()
        {
            return _districtName;
        }

    }
}
