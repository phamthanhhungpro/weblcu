using Datas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Datas.Models.DomainModels
{
    public class Location : AbstractData
    {
        [Display(Name = "Vĩ độ (lat)")]
        public string? Lat { set; get; }

        [Display(Name = "Kinh độ (long)")]
        public string? Long { set; get; }

        [Display(Name = "Dân tộc")]
        public int? PeopleId { set; get; }

        [Display(Name = "Dân tộc")]
        public virtual People People { set; get; }

        [Display(Name = "Thành phố/huyện")]
        public int? DistrictId { get; set; }
        [Display(Name = "Thành phố/huyện")]
        public virtual District District { set; get; }

        [Display(Name = "Phường/xã")]
        public int? WardId { get; set; }
        [Display(Name = "Phường/xã")]
        public virtual Ward Ward { set; get; }

        public LocationModel ToLocationModel()
        {
            var data = new LocationModel
            {
                Id = Id,
                Lat = Lat,
                Long = Long,
                Status = Status,
                PeopleId = PeopleId,
                DistrictId = DistrictId,
                WardId = WardId
            };
            if (Ward != null)
            {
                data.SetWardName(Ward.Name);
            }
            if (District != null)
            {
                data.SetDistrictName(District.Name);
            }
            return data;
        }
    }
}
