using System.ComponentModel.DataAnnotations;

namespace Datas.Models.ViewModels
{
    public class TravelModel : BaseModelViewModel
    {

        private string _peopleName = string.Empty;

        [Required(ErrorMessage = "Bạn cần nhập tên hoạt động du lịch")]
        [Display(Name = "Tên hoạt động du lịch")]
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
