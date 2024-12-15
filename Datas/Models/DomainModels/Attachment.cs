using Datas.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Datas.Models.DomainModels
{
    public class Attachment : AbstractData
    {
        [Display(Name = "Đường dẫn")]
        public string? Path { set; get; }

        [Display(Name = "FileName")]
        public string? Name { set; get; }

        [Display(Name = "Dân tộc")]
        public int? PeopleId { set; get; }

        [Display(Name = "Dân tộc")]
        public virtual People People { set; get; }

        [Display(Name = "Trang phục")]
        public int? NationalCostumeId { set; get; }

        [Display(Name = "Trang phục")]
        public virtual NationalCostume NationalCostume { set; get; }
        
        [Display(Name = "Nhạc cụ")]
        public int? InstrumentId { set; get; }

        [Display(Name = "Nhạc cụ")]
        public virtual Instrument? Instrument { set; get; }

        [Display(Name = "Công cụ sản xuất")]
        public int? ProduceToolId { set; get; }

        [Display(Name = "Công cụ sản xuất")]
        public virtual ProduceTool? ProduceTool { set; get; }

        [Display(Name = "Phong tục tập quán")]
        public int? CustomsTraditionId { set; get; }

        [Display(Name = "Phong tục tập quán")]
        public virtual CustomsTradition? CustomsTradition { set; get; }

        [Display(Name = "Lễ hội văn hóa")]
        public int? FestivalId { set; get; }

        [Display(Name = "Lễ hội văn hóa")]
        public virtual Festival? Festival { set; get; }
        
        [Display(Name = "Di tích, danh lam")]
        public int? LandmarkId { set; get; }

        [Display(Name = "Di tích, danh lam")]
        public virtual Landmark? Landmark { set; get; }

        [Display(Name = "Hoạt động du lịch")]
        public int? TravelId { set; get; }

        [Display(Name = "Hoạt động du lịch")]
        public virtual Travel? Travel { set; get; }
        
        [Display(Name = "Trang sức")]
        public int? JewelryId { set; get; }

        [Display(Name = "Trang sức")]
        public virtual Jewelry? Jewelry { set; get; }


        [Display(Name = "Vật dụng hàng ngày")]
        public int? DailyItemId { set; get; }
        [Display(Name = "Vật dụng hàng ngày")]
        public virtual DailyItem? DailyItem { set; get; }


        public AttachmentModel ToAttachmentModel()
        {
            var data = new AttachmentModel
            {
                Id = Id,
                Path = Path,
                Name = Name,
                Status = Status,
                PeopleId = PeopleId,
                NationalCostumeId = NationalCostumeId,
                ProduceToolId = ProduceToolId,
                CustomsTraditionId = CustomsTraditionId,
                FestivalId = FestivalId,
                InstrumentId = InstrumentId,
                JewelryId = JewelryId,
                LandmarkId = LandmarkId,
                TravelId = TravelId,
                DailyItemId = DailyItemId,
            };
            
            return data;
        }
    }
}
