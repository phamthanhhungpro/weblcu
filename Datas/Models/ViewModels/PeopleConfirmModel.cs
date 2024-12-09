using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enums;

namespace Datas.Models.ViewModels
{
    public class PeopleConfirmModel : AbstractData
    {
        [Display(Name = "Nội dung")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        [Display(Name = "Dân tộc")]
        public int? PeopleId { set; get; }

        [Display(Name = "Trạng thái")]
        public PeopleConfirmStatus ConfirmStatus { set; get; } = PeopleConfirmStatus.Update;
    }
}
