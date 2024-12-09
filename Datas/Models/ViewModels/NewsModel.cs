using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class NewsModel : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tiêu đề")]
        [Display(Name = "Tiêu đề")]
        public required string Title { get; set; }

        [Display(Name = "Tác giả")]
        public string? Author { get; set; }

        [Display(Name = "Từ khóa")]
        public string? KeyWord { get; set; }

        [Display(Name = "Lên đầu")]
        public bool Top { get; set; } = false;

        [Display(Name = "Mô tả")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        [Display(Name = "Thời gian đăng")]
        [Required(ErrorMessage = "Bạn cần chọn thời gian")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PostDate { get; set; } = DateTime.Now;

        [Display(Name = "Hình minh họa")]
        [Required(ErrorMessage = "Bạn cần chọn hình minh họa")]
        public required string Image { get; set; }

        [Display(Name = "Chuyên mục")]
        [Required(ErrorMessage = "Bạn cần chọn chuyên mục")]
        public int? CategoryId { get; set; }

        [Display(Name = "Nội dung bài viết")]
        [Required(ErrorMessage = "Bạn cần nhập nội dung bài viết")]
        public required string Content { get; set; }

        [Display(Name = "Địa chỉ")]
        public string? Location { set; get; }

        [Display(Name = "Vĩ độ (lat)")]
        public string? Lat { set; get; }

        [Display(Name = "Kinh độ (long)")]
        public string? Long { set; get; }

        [Display(Name = "Điện thoại")]
        public string? Phone { set; get; }

        [Display(Name = "Lịch")]
        public string? Calendar { set; get; }

        public int View { get; set; }
        public string? Url { set; get; }
    }
}
