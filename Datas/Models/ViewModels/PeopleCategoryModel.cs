using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class PeopleCategoryModel : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên loại dân tộc")]
        [Display(Name = "Loại dân tộc")]
        public string Name { get; set; }

        [Display(Name = "Hình minh họa")]
        public string? Image { get; set; }

        [Display(Name = "Từ khóa")]
        public string? KeyWord { get; set; }

        [Display(Name = "Mô tả")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        [Display(Name = "Loại dân tộc cha")]
        public int? ParentId { get; set; }

        public string? Url { get; set; }
    }
}
