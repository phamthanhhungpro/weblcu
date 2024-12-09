using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class SettingModel
    {
        [Display(Name = "Link ảnh 360")]
        public string? ImageLink { set; get; } = string.Empty;

        [Display(Name = "Tiêu đề")]
        public string? FooterTitle { set; get; } = string.Empty;

        [Display(Name = "Cơ quan chủ quản")]
        public string? FooterAgency { set; get; } = string.Empty;

        [Display(Name = "Địa chỉ")]
        public string? FooterAddress { set; get; } = string.Empty;

        [Display(Name = "Chịu trách nhiệm chính")]
        public string? FooterAuthor { set; get; } = string.Empty;

        [Display(Name = "Số điện thoại")]
        public string? FooterPhone { set; get; } = string.Empty;

        [Display(Name = "Email")]
        public string? FooterEmail { set; get; } = string.Empty;

        [Display(Name = "Facebook")]
        public string? FollowFacebook { set; get; } = string.Empty;

        [Display(Name = "Tiktok")]
        public string? FollowTiktok { set; get; } = string.Empty;

        [Display(Name = "YouTube")]
        public string? FollowYouTube { set; get; } = string.Empty;

        [Display(Name = "Về chúng tôi")]
        public string? OwnerDetails { set; get; } = string.Empty;

        [Display(Name = "Địa chỉ")]
        public string? ContactAddress { set; get; } = string.Empty;

        [Display(Name = "Số điện thoại")]
        public string? ContactPhone { set; get; } = string.Empty;

        [Display(Name = "Email")]
        public string? ContactEmail { set; get; } = string.Empty;

        [Display(Name = "Link")]
        public string? ContactLink { set; get; } = string.Empty;

        [Display(Name = "Tài khoản email")]
        public string? EmailAccount { set; get; } = string.Empty;

        [Display(Name = "Mật khẩu email")]
        public string? EmailPass { set; get; } = string.Empty;

        [Display(Name = "Tên hiển thị")]
        public string? EmailName { set; get; } = string.Empty;

        [Display(Name = "Máy chủ")]
        public string? EmailServer { set; get; } = string.Empty;

        [Display(Name = "Cổng kết nối")]
        public string? EmailPort { set; get; } = string.Empty;

    }
}
