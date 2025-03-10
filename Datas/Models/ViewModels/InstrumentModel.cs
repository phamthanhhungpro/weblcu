using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Datas.Models.ViewModels
{
    public class InstrumentModel : BaseModelViewModel
    {
        private string _categoryName = string.Empty;
        private string _peopleName = string.Empty;

        [Required(ErrorMessage = "Bạn cần nhập tên nhạc cụ")]
        [Display(Name = "Tên nhạc cụ")]
        public string Name { set; get; }
        public string? TenGoiKhac { set; get; }

        [Display(Name = "Loại nhạc cụ")]
        public int? CategoryId { get; set; }

        public string? Structure { get; set; }

        public string? Dimensions { get; set; }

        public string? PlayingMethod { get; set; }

        public string? PitchRange { get; set; }
        public string? Timbre { get; set; }
        public string? RoleInOrchestra { get; set; }
        public string? PerformanceEvents { get; set; }
        public string? Origin { get; set; }
        public string? CulturalSignificance { get; set; }
        public string? FamousArtisans { get; set; }
        public string? VungMien { get; set; }

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