using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class UserInfoModel : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần điền tên đầy đủ của người dùng")]
        [Display(Name = "Họ tên")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Định dạng email không chính xác")]
        public string Email { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Năm sinh")]
        public DateTime? Birthday { get; set; }

        [Display(Name = "SĐT")]
        public string? PhoneNumber { get; set; }
    }
}
