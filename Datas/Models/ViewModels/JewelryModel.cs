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

        [Display(Name = "Loại trang sức")]
        public int? CategoryId { get; set; }

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
