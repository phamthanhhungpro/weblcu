using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class LanguageModel : AbstractData
    {

        [Required(ErrorMessage = "Bạn cần nhập tên ngôn ngữ")]
        [Display(Name = "Tên ngôn ngữ")]
        public string Name { set; get; }

        [Display(Name = "Chi tiết")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }
    }
}
