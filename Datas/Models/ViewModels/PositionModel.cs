using Datas.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class PositionModel : AbstractData
    {
        [Required(ErrorMessage = "Cần nhập tên chức vụ")]
        [Display(Name = "Tên chức vụ")]
        public string Name { set; get; }

        [Display(Name = "Chi tiết chức vụ")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        public Position ToPosition()
        {
            return new Position()
            {
                Name = Name,
                Details = Details,
                Status = Status,
            };
        }
    }
}
