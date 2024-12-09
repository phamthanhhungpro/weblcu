using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class CompanyModel : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên đơn vị")]
        [Display(Name = "Tên đơn vị")]
        public string Name { get; set; }

        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }

        [Display(Name = "SĐT")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Đơn vị cha")]
        public int? ParentId { get; set; }
    }
}
