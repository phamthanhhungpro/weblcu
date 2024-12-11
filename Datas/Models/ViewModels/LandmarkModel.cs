using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Datas.Models.ViewModels
{
    public class LandmarkModel : BaseModelViewModel
    {
        private string _peopleName = string.Empty;

        [Required(ErrorMessage = "Bạn cần nhập tên điểm đến di tích, danh lam")]
        [Display(Name = "Tên điểm đến di tích, danh lam")]
        public string Name { set; get; }

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