using Datas.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class AttachmentModel : AbstractData
    {
        [Display(Name = "Đường dẫn")]
        public string? Path { set; get; }

        [Display(Name = "FileName")]
        public string? Name { set; get; }

        [Display(Name = "Dân tộc")]
        public int? PeopleId { set; get; }

        [Display(Name = "Trang phục")]
        public int? NationalCostumeId { set; get; }

        [Display(Name = "Công cụ sản xuất")]
        public int? ProduceToolId { set; get; }

        [Display(Name = "Phong tục tập quán")]
        public int? CustomsTraditionId { set; get; }

        [Display(Name = "Lễ hội văn hóa")]
        public int? FestivalId { set; get; }
        
        [Display(Name = "Nhạc cụ")]
        public int? InstrumentId { set; get; }
        
        [Display(Name = "Di tích, danh lam")]
        public int? LandmarkId { set; get; }

        [Display(Name = "Hoạt động du lịch")]
        public int? TravelId { set; get; }

        [Display(Name = "Trang sức")]
        public int? JewelryId { set; get; }

    }
}
