using System.ComponentModel.DataAnnotations;

namespace Datas.Models.ViewModels
{
    public class ProduceToolModel : BaseModelViewModel
    {

        private string _categoryName = string.Empty;
        private string _peopleName = string.Empty;

        [Required(ErrorMessage = "Bạn cần nhập tên công cụ sản xuất")]
        [Display(Name = "Tên công cụ sản xuất")]
        public string Name { set; get; }
        public string? TenGoiKhac { set; get; }

        [Display(Name = "Loại công cụ sản xuất")]
        public int? CategoryId { get; set; }

        public string? Structure { get; set; }
        public string? Dimensions { get; set; }
        public string? Usage { get; set; }
        public string? MainFunction { get; set; }
        public string? RelatedTraditionalCrafts { get; set; }
        public string? UsageEfficiency { get; set; }
        public string? Variants { get; set; }
        public string? Origin { get; set; }
        public string? CulturalSignificance { get; set; }
        public string? FamousArtisans { get; set; }
        public string? TinhTrangHienTai { get; set; }
        public string? Custodian { get; set; }
        public string? EconomicValue { get; set; }
        public string? VungMien { get; set; }
        public string? NganhNgheSuDung { get; set; }

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
