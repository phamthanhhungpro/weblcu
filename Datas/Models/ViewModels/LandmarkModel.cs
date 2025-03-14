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

        [Display(Name = "Tên gọi khác")]
        public string? AlternativeNames { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mã định danh")]
        [Display(Name = "Mã di tích")]
        public string IdentityCode { get; set; }

        [Display(Name = "Loại di tích")]
        public string? LandmarkType { get; set; }

        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }

        [Display(Name = "Tọa độ GPS")]
        public string? GpsCoordinates { get; set; }

        [Display(Name = "Nguồn gốc & Lịch sử")]
        [DataType(DataType.MultilineText)]
        public string? History { get; set; }

        [Display(Name = "Sự kiện liên quan")]
        [DataType(DataType.MultilineText)]
        public string? RelatedEvents { get; set; }

        [Display(Name = "Tín ngưỡng & Tâm linh")]
        [DataType(DataType.MultilineText)]
        public string? SpiritualBelief { get; set; }

        [Display(Name = "Giá trị văn hóa - xã hội")]
        [DataType(DataType.MultilineText)]
        public string? CulturalValue { get; set; }

        [Display(Name = "Kiến trúc/Tự nhiên")]
        [DataType(DataType.MultilineText)]
        public string? Architecture { get; set; }

        [Display(Name = "Chất liệu xây dựng")]
        public string? BuildingMaterial { get; set; }

        [Display(Name = "Tình trạng hiện tại")]
        [DataType(DataType.MultilineText)]
        public string? CurrentCondition { get; set; }

        [Display(Name = "Diện tích & Quy mô")]
        public string? SizeScale { get; set; }

        [Display(Name = "Hiện vật trưng bày")]
        [DataType(DataType.MultilineText)]
        public string? Exhibits { get; set; }

        [Display(Name = "Thời gian mở cửa")]
        public string? OpeningHours { get; set; }

        [Display(Name = "Giá vé")]
        public string? TicketPrice { get; set; }

        [Display(Name = "Các dịch vụ hỗ trợ")]
        [DataType(DataType.MultilineText)]
        public string? SupportServices { get; set; }

        [Display(Name = "Hoạt động trải nghiệm")]
        [DataType(DataType.MultilineText)]
        public string? ExperienceActivities { get; set; }

        [Display(Name = "Lượng khách tham quan hàng năm")]
        public int? AnnualVisitors { get; set; }

        [Display(Name = "Xếp hạng di tích")]
        public string? HeritageRanking { get; set; }

        [Display(Name = "Cơ quan quản lý")]
        public string? ManagementAuthority { get; set; }

        [Display(Name = "Dự án bảo tồn & Phục dựng")]
        [DataType(DataType.MultilineText)]
        public string? ConservationProject { get; set; }

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