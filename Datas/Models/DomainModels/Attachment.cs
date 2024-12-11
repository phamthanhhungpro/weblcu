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

        [Display(Name = "Công cụ sản xuất")]
        public int? ProduceToolId { set; get; }

        [Display(Name = "Công cụ sản xuất")]
        public virtual ProduceTool? ProduceTool { set; get; }

        [Display(Name = "Phong tục tập quán")]
        public int? CustomsTraditionId { set; get; }

        [Display(Name = "Phong tục tập quán")]
        public virtual CustomsTradition? CustomsTradition { set; get; }

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
                CustomsTraditionId = CustomsTraditionId
            };
            
            return data;
        }
    }
}
