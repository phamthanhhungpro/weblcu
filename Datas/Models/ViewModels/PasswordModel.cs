using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class PasswordModel : AbstractData
    {
        [RegularExpression(@"^.*(?=.{6,})(?=.*[a-z])(?=.*[A-Z])(?=.*[\d\W]).*$", ErrorMessage = "Mật khẩu phải có chữ hoa, chữ thường, và ký tự đặc biệt")]
        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Bạn cần điền mật khẩu mới")]
        public string NewPassword { get; set; }
        [Display(Name = "Mật khẩu cũ")]
        [Required(ErrorMessage = "Bạn cần điền mật khẩu cũ")]
        public string OldPassword { get; set; }
        [Display(Name = "Xác nhận mật khẩu mới")]
        [Required(ErrorMessage = "Bạn cần xác nhận mật khẩu mới")]
        public string ConfirmNewPassword { get; set; }

        public string EncodeNewPassword
        {
            get
            {
                return Utilities.CalculateMD5Hash(NewPassword);
            }
        }

        public string EncodeOldPassword
        {
            get
            {
                return Utilities.CalculateMD5Hash(OldPassword);
            }
        }
    }
}
