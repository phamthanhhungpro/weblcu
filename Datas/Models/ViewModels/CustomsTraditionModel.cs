using System.ComponentModel.DataAnnotations;

namespace Datas.Models.ViewModels
{
    public class CustomsTraditionModel : BaseModelViewModel
    {

        private string _peopleName = string.Empty;

        [Required(ErrorMessage = "Bạn cần nhập tên phong tục tập quán")]
        [Display(Name = "Tên phong tục tập quán")]
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
