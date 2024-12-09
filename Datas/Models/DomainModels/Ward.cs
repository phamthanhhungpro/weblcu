using Common;
using Datas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.DomainModels
{
    public class Ward : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên phường/xã")]
        [Display(Name = "Tên phường/xã")]
        public string Name { set; get; }

        [Display(Name = "Chi tiết")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        [Display(Name = "Trực thuộc thành phố/huyện")]
        public int? DistrictId { get; set; }
        [Display(Name = "Trực thuộc thành phố/huyện")]
        public virtual District District { set; get; }

        public virtual ICollection<Location> Locations { set; get; }

        public override bool IsExistAnother()
        {
            return Locations.Any(o => o.DeleteStatus == Enums.DeleteStatus.Normal);
        }

        public void SetNewData(Ward model)
        {
            Name = model.Name;
            Details = model.Details;
            Status = model.Status;
        }

        public void SetNewData(WardModel model)
        {
            Name = model.Name;
            Details = model.Details;
            Status = model.Status;
        }

        public WardModel ToWardModel()
        {
            var model = new WardModel
            {
                Id = Id,
                Name = Name,
                Details = Details,
                Status = Status,
                DistrictId = DistrictId
            };
            return model;
        }
    }
}
