using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class Image360Model : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tiêu đề")]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [Display(Name = "Hiển thị")]
        public bool IsDisplay { get; set; } = true;

        [Required(ErrorMessage = "Bạn cần nhập ảnh đại diện")]
        [Display(Name = "Ảnh đại diện")]
        public string Image { get; set; }

        [Display(Name = "Ảnh 360")]
        public string? ImageLink { get; set; }

        [Display(Name = "Nội dung")]
        [DataType(DataType.MultilineText)]
        public string? Details { get; set; }
    }
}
