using Common;
using Datas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class InstrumentCategoryModel : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên loại nhạc cụ")]
        [Display(Name = "Loại nhạc cụ")]
        public string Name { get; set; }

        [Display(Name = "Hình minh họa")]
        public string? Image { get; set; }

        [Display(Name = "Từ khóa")]
        public string? KeyWord { get; set; }

        [Display(Name = "Mô tả")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        [Display(Name = "Loại nhạc cụ cha")]
        public int? ParentId { get; set; }
    }
}