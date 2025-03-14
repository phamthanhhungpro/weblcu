using System.ComponentModel.DataAnnotations;

namespace Datas.Models.ViewModels
{
    public class JewelryModel : BaseModelViewModel
    {
        private string _categoryName = string.Empty;
        private string _peopleName = string.Empty;

        [Required(ErrorMessage = "Bạn cần nhập tên trang sức")]
        [Display(Name = "Tên trang sức")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập mã định danh")]
        [Display(Name = "Mã trang sức")]
        public string IdentityCode { get; set; }

        [Display(Name = "Tên gọi khác")]
        public string? AlternativeNames { get; set; }

        [Display(Name = "Loại trang sức")]
        public int? CategoryId { get; set; }

        [Display(Name = "Giới tính sử dụng")]
        public string? Gender { get; set; }

        [Display(Name = "Độ tuổi sử dụng")]
        public string? AgeGroup { get; set; }

        [Display(Name = "Link ảnh scan 3D")]
        public string? Scan3DLink { get; set; }

        [Display(Name = "Chất liệu chính")]
        public string? MainMaterial { get; set; }

        [Display(Name = "Phong cách thiết kế")]
        public string? DesignStyle { get; set; }

        [Display(Name = "Kích thước & Trọng lượng")]
        public string? SizeWeight { get; set; }

        [Display(Name = "Mục đích sử dụng")]
        public string? Purpose { get; set; }

        [Display(Name = "Ý nghĩa biểu tượng")]
        public string? SymbolicMeaning { get; set; }

        [Display(Name = "Sự kiện liên quan")]
        public string? RelatedEvents { get; set; }

        [Display(Name = "Ảnh hưởng văn hóa - xã hội")]

        public string? CulturalImpact { get; set; }

        [Display(Name = "Nguồn gốc")]

        public string? Origin { get; set; }

        [Display(Name = "Sự thay đổi theo thời gian")]
        public string? HistoricalChanges { get; set; }

        [Display(Name = "Khu vực phổ biến")]
        public string? CommonRegions { get; set; }

        [Display(Name = "Cách bảo quản")]
        public string? PreservationMethod { get; set; }

        [Display(Name = "Hiện vật trưng bày")]
        public string? DisplayedItems { get; set; }

        public void SetCategoryName(string data)
        {
            _categoryName = data;
        }

        public string GetCategoryName()
        {
            return _categoryName;
        }

        public void SetPeopleName(string data)
        {
            _peopleName = data;
        }

        public string GetPeopleName()
        {
            return _peopleName;
        }
    }
}
