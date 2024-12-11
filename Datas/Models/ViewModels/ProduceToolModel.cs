using Datas.Models.DomainModels;
using System.ComponentModel.DataAnnotations;

namespace Datas.Models.ViewModels
{
    public class ProduceToolModel : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên")]
        [Display(Name = "Tên")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Từ khóa")]
        public string? KeyWord { get; set; }

        [Display(Name = "Mô tả")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        [Display(Name = "Hình minh họa")]
        public string? Image { get; set; } = string.Empty;

        [Display(Name = "Chuyên mục")]
        public int? CategoryId { get; set; }

        [Display(Name = "Chuyên mục")]
        public virtual ProduceToolCategory? Category { get; set; }

        public int View { get; set; }

        public string? Url { set; get; }
    }
}
