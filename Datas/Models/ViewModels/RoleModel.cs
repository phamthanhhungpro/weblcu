using Datas.Entity;
using Datas.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class RoleModel : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên nhóm quyền")]
        [Display(Name = "Tên nhóm quyền")]
        public string Name { get; set; }

        [Display(Name = "Ghi chú")]
        public string? Title { get; set; }

        [Display(Name = "Danh sách quyền")]
        public List<AssignFunction> Functions { get; set; }


        public Role ToRole()
        {
            return new Role
            {
                Name = Name,
                Title = Title,
                Status = Status,
            };
        }
    }
}
