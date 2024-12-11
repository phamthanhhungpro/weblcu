using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Datas.Models.ViewModels
{
    public class InstrumentModel : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên nhạc cụ")]
        [Display(Name = "Tên nhạc cụ")]
        public required string Name { get; set; }
        
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
        
        public int View { get; set; }
        public string? Url { set; get; }
    }
}