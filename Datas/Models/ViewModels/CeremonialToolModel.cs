using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Datas.Models.ViewModels
{
    public class CeremonialToolModel : BaseModelViewModel
    {
        private string _categoryName = string.Empty;
        private string _peopleName = string.Empty;

        [Required(ErrorMessage = "Bạn cần nhập tên dụng cụ nghi lễ")]
        [Display(Name = "Tên dụng cụ nghi lễ")]
        public string Name { set; get; }

        [Display(Name = "Loại dụng cụ nghi lễ")]
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