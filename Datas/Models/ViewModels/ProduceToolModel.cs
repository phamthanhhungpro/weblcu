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

        [Display(Name = "Loại công cụ sản xuất")]
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
