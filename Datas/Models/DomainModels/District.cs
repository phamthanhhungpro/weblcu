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
    public class District : AbstractData
    {

        [Required(ErrorMessage = "Bạn cần nhập tên thành phố/huyện")]
        [Display(Name = "Tên thành phố/huyện")]
        public string Name { set; get; }

        [Display(Name = "Chi tiết")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        public virtual ICollection<Ward> Wards { set; get; }

        public virtual ICollection<Location> Locations { set; get; }

        public override bool IsExistAnother()
        {
            return Wards.Any(o => o.DeleteStatus == Enums.DeleteStatus.Normal) || Locations.Any(o => o.DeleteStatus == Enums.DeleteStatus.Normal);
        }

        public void SetNewData(District model)
        {
            Name = model.Name;
            Details = model.Details;
            Status = model.Status;
        }

        public void SetNewData(DistrictModel model)
        {
            Name = model.Name;
            Details = model.Details;
            Status = model.Status;
        }

        public DistrictModel ToDistrictModel()
        {
            var model = new DistrictModel
            {
                Id = Id,
                Name = Name,
                Details = Details,
                Status = Status
            };
            return model;
        }
    }
}
