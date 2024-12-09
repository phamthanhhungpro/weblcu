using Datas.Entity;
using Datas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Datas.Models.DomainModels
{
    public class User : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần điền tên đăng nhập")]
        [RegularExpression(@"^[a-zA-Z0-9_\-.]+$", ErrorMessage = "Tên đăng nhập chỉ gồm ký tự, chữ số và ký tự -")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bạn cần điền mật khẩu")]
        //[RegularExpression(@"^.*(?=.{6,})(?=.*[a-z])(?=.*[A-Z])(?=.*[\d\W]).*$", ErrorMessage = "Mật khẩu phải có chữ hoa, chữ thường, và ký tự đặc biệt")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bạn cần điền tên đầy đủ của người dùng")]
        [Display(Name = "Họ tên")]
        public string FullName { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [Display(Name = "SĐT")]
        public string? PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Định dạng email không chính xác")]
        public string? Email { get; set; } = string.Empty;

        [Display(Name = "Đơn vị")]
        public int? UserCompanyId { get; set; }
        public virtual Company UserCompany { get; set; }

        [Display(Name = "Phòng tổ")]
        public int? DepartmentCompanyId { get; set; }
        public virtual Department DepartmentCompany { get; set; }

        [Display(Name = "Chức vụ")]
        public int? UserPositionId { get; set; }
        public virtual Position UserPosition { get; set; }
        public virtual ICollection<Role> UserRoles { get; set; }
        public bool SuperAdmin { set; get; } = false;
        public UserInfo ToUserInfo()
        {
            return new UserInfo
            {
                Id = Id,
                UserName = UserName,
                FullName = FullName,
                SuperAdmin = SuperAdmin
            };
        }
        public void SetNewData(UserModel user)
        {
            this.UserName = user.UserName;
            this.FullName = user.FullName;
            this.Birthday = user.Birthday;
            this.PhoneNumber = user.PhoneNumber;
            this.Email = user.Email;
            this.Status = user.Status;
        }

        public void SetNewData(UserInfoModel user)
        {
            this.FullName = user.FullName;
            this.Birthday = user.Birthday;
            this.PhoneNumber = user.PhoneNumber;
            this.Email = user.Email;
        }

        public UserInfoModel ToUserInfoModel()
        {
            var userModel = new UserInfoModel
            {
                Id = Id,
                FullName = FullName,
                Birthday = Birthday,
                PhoneNumber = PhoneNumber,
                Email = Email
            };
            return userModel;
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

        public UserModel ToUserModel()
        {
            var userModel = new UserModel
            {
                Id = Id,
                FullName = FullName,
                Birthday = Birthday,
                PhoneNumber = PhoneNumber,
                UserName = UserName,
                Email = Email,
                Status = Status
            };

            if (UserCompany != null)
            {
                userModel.CompanyId = UserCompany.Id;
            }
            if (DepartmentCompany != null)
            {
                userModel.DepartmentId = DepartmentCompany.Id;
            }
            return userModel;
        }
    }
}
