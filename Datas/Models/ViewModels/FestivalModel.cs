using System.ComponentModel.DataAnnotations;

namespace Datas.Models.ViewModels
{
    public class FestivalModel : BaseModelViewModel
    {

        private string _peopleName = string.Empty;
        public string? TenGoiKhac { get; set; }
        public string? VungMien { get; set; }
        public string? ThoiGianToChuc { get; set; }
        public string? Purpose { get; set; }
        public string? OrganizationForm { get; set; }
        public string? Participants { get; set; }
        public string? MainEvents { get; set; }
        public string? Origin { get; set; }
        public string? ChangesOverTime { get; set; }
        public string? RelatedBeliefs { get; set; }
        public string? CulturalSocialImpact { get; set; }
        public string? MainRituals { get; set; }
        public string? TraditionalCostumes { get; set; }
        public string? OfferingsAndWorship { get; set; }
        public string? FolkGames { get; set; }
        public string? ConservationStatus { get; set; }
        public string? OrganizingAgency { get; set; }
        public string? ConservationActivities { get; set; }
        public string? TTBaoTon { get; set; }

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
