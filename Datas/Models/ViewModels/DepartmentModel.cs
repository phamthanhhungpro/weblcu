using Datas.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class DepartmentModel : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên phòng tổ")]
        [Display(Name = "Tên phòng tổ")]
        public string Name { get; set; }

        [Display(Name = "Ghi chú")]
        public string? Title { get; set; }

        [Display(Name = "Đơn vị")]
        public int? CompanyId { get; set; }

        [Display(Name = "Phòng tổ cha")]
        public int? ParentId { get; set; }

        public Department ToDepartment()
        {
            return new Department
            {
                Name = Name,
                Title = Title,
                Status = Status,
            };
        }
    }
}
