using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.DomainModels
{
    public class Function : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên quyền")]
        [Display(Name = "Tên quyền")]
        public string Name { get; set; }

        [Display(Name = "Mã quyền")]
        public string FunctionCode { get; set; }

        [Display(Name = "Nhóm")]
        public virtual GroupFunction Group { get; set; }

        public virtual ICollection<Role> FunctionRoles { get; set; }

    }
}
