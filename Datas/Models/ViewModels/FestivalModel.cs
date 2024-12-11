using System.ComponentModel.DataAnnotations;

namespace Datas.Models.ViewModels
{
    public class FestivalModel : BaseModelViewModel
    {

        private string _peopleName = string.Empty;

        [Required(ErrorMessage = "Bạn cần nhập tên lễ hội văn hóa")]
        [Display(Name = "Tên lễ hội văn hóa")]
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
