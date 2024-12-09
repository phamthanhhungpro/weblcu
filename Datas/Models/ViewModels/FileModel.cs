using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class FileModel : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên tài liệu")]
        [Display(Name = "Tên tài liệu")]
        public string Name { set; get; }

        [Display(Name = "Chi tiết")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        [Display(Name = "File đính kèm")]
        public string? FilePath { get; set; } = string.Empty;

        [Display(Name = "Nội dung tài liệu")]
        public string? Content { get; set; } = string.Empty;

        [Display(Name = "Quyền truy cập tài liệu")]
        public Enums.AccessStatus Access { get; set; } = Enums.AccessStatus.Public;

        [Display(Name = "Danh sách truy cập")]
        public string? AccessData { get; set; } = string.Empty;

        public int? Owner { get; set; }
    }
}
