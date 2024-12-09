using Datas.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class WardModel : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên phường/xã")]
        [Display(Name = "Tên phường/xã")]
        public string Name { set; get; }

        [Display(Name = "Chi tiết")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        [Required(ErrorMessage = "Bạn cần chọn thành phố/huyện")]
        [Display(Name = "Trực thuộc thành phố/huyện")]
        public int? DistrictId { set; get; }

        public Ward ToWard()
        {
            return new Ward()
            {
                Name = Name,
                Details = Details,
                Status = Status,
            };
        }
    }
}
