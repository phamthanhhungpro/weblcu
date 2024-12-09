using Datas.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class UserModel : AbstractData
    {

        [Required(ErrorMessage = "Bạn cần điền tên đăng nhập")]
        [RegularExpression(@"^[a-zA-Z0-9_\-.]+$", ErrorMessage = "Tên đăng nhập chỉ gồm ký tự, chữ số và ký tự -")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Bạn cần điền tên đầy đủ của người dùng")]
        [Display(Name = "Họ tên")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Định dạng email không chính xác")]
        public string? Email { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [Display(Name = "SĐT")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Chức vụ")]
        public int? PositionId { get; set; }

        [Display(Name = "Đơn vị")]
        public int? CompanyId { get; set; }

        [Display(Name = "Phòng tổ")]
        public int? DepartmentId { get; set; }

        public User ToUser()
        {
            return new User
            {
                Id = Id,
                FullName = FullName,
                Birthday = Birthday,
                PhoneNumber = PhoneNumber,
                UserName = UserName,
                Email = Email,
                Status = Status
            };
        }
        public User ToUserCreate()
        {
            return new User
            {
                Password = Password,
                Id = Id,
                FullName = FullName,
                Birthday = Birthday,
                PhoneNumber = PhoneNumber,
                UserName = UserName,
                Email = Email,
                Status = Status
            };
        }
    }
}
