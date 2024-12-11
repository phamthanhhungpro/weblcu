using Datas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

        [Display(Name = "Trang phục")]
        public int? ProduceToolId { set; get; }

        [Display(Name = "Công cụ sản xuất")]
        public virtual ProduceTool ProduceTool { set; get; }

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
            };
            
            return data;
        }
    }
}
