using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.DomainModels
{
    public class GroupFunction : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên nhóm quyền")]
        [Display(Name = "Tên quyền")]
        public required string Name { get; set; }

        public Enums.GroupFunctionType Type { get; set; }

        public int Order { get; set; }

        public virtual ICollection<Function> Functions { get; set; }
    }
}
