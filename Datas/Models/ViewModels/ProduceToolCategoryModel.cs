using System.ComponentModel.DataAnnotations;

namespace Datas.Models.ViewModels
{
    public class ProduceToolCategoryModel : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên danh mục công cụ sản xuất")]
        [Display(Name = "Tên danh mục công cụ sản xuất")]
        public string Name { get; set; }

        [Display(Name = "Hình minh họa")]
        public string? Image { get; set; }

        [Display(Name = "Từ khóa")]
        public string? KeyWord { get; set; }

        [Display(Name = "Mô tả")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        [Display(Name = "Danh mục cha")]
        public int? ParentId { get; set; }
    }
}
