using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class LoginModel
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Bạn cần điền tên đăng nhập")]
        [RegularExpression(@"^[a-zA-Z0-9_\-.]+$", ErrorMessage = "Tên đăng nhập chỉ gồm ký tự, chữ số và ký tự -")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Bạn cần điền mật khẩu")]
        [RegularExpression(@"^(?=.*[A-Za-z0-9])[\S]*$", ErrorMessage = "Mật khẩu phải có chữ hoa, chữ thường, và ký tự đặc biệt")]
        public string Password { get; set; }

        public string EncodePassWord
        {
            get
            {
                return Utilities.CalculateMD5Hash(Password);
            }
        }
    }
}
