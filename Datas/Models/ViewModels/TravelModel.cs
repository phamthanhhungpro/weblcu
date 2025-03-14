using System.ComponentModel.DataAnnotations;

namespace Datas.Models.ViewModels
{
    public class TravelModel : BaseModelViewModel
    {

        private string _peopleName = string.Empty;

        [Required(ErrorMessage = "Bạn cần nhập tên hoạt động du lịch")]
        [Display(Name = "Tên hoạt động du lịch")]
        public string Name { set; get; }
        public string? DiaDiem { set; get; }
        public string? LoaiHinh { set; get; }
        public string? ThoiGian { set; get; }
        public string? DonViToChuc { set; get; }

        public string? Highlights { get; set; }
        public string? UniqueExperiences { get; set; }
        public string? CulturalSocialImpact { get; set; }
        public string? AccompanyingEvents { get; set; }

        // Fields for Chi phí tham gia, Dịch vụ đi kèm, Ưu đãi & Khuyến mãi
        public string? ParticipationCost { get; set; }
        public string? AccompanyingServices { get; set; }
        public string? DiscountsAndPromotions { get; set; }

        // Fields for Tình trạng hoạt động, Cơ quan quản lý, Hoạt động quảng bá
        public string? OperationalStatus { get; set; }
        public string? ManagingAgency { get; set; }
        public string? PromotionalActivities { get; set; }
        public string? DetailedDescription { get; set; }
        public string? Participants { get; set; }
        public string? Suitability { get; set; }
        public string? RequiredAttireAndEquipment
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
