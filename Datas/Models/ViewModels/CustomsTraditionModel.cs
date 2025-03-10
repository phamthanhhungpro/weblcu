using System.ComponentModel.DataAnnotations;

namespace Datas.Models.ViewModels
{
    public class CustomsTraditionModel : BaseModelViewModel
    {

        private string _peopleName = string.Empty;

        [Required(ErrorMessage = "Bạn cần nhập tên phong tục tập quán")]
        [Display(Name = "Tên phong tục tập quán")]
        public string Name { set; get; }
        public string? TenGoiKhac { set; get; }
        public string? PurposeMeaning { get; set; }
        public string? TimeOfOccurrence { get; set; }
        public string? FormOfImplementation { get; set; }
        public string? Participants { get; set; }
        public string? Origin { get; set; }
        public string? ChangesOverTime { get; set; }
        public string? RelatedBeliefs { get; set; }
        public string? SocialImpact { get; set; }
        public string? RequiredItems { get; set; }
        public string? TraditionalAttire { get; set; }
        public string? Steps { get; set; }
        public string? ConservationStatus { get; set; }
        public string? ConservationUnit { get; set; }
        public string? VungMien { get; set; }
        public string? PreservationActivities
        {
            get; set;
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
