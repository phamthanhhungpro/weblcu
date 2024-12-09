using Datas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Datas.Models.DomainModels
{
    public class Role : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên chức vụ")]
        [Display(Name = "Tên nhóm quyền")]
        public string Name { get; set; }

        [Display(Name = "Ghi chú")]
        public string? Title { get; set; }

        [Display(Name = "Danh sách quyền")]
        public virtual ICollection<Function> RoleFunctions { get; set; }

        public virtual ICollection<User> RoleUsers { get; set; }

        public override bool IsExistAnother()
        {
            return RoleUsers.Any();
        }

        public void SetNewData(RoleModel model)
        {
            Name = model.Name;
            Title = model.Title;
            Status = model.Status;
        }

        public void SetNewData(Role model)
        {
            Name = model.Name;
            Title = model.Title;
            Status = model.Status;
        }
        public RoleModel ToRoleModel()
        {
            var RoleModel = new RoleModel
            {
                Name = Name,
                Title = Title,
                Status = Status,
                Functions = RoleFunctions.Select(o => new Entity.AssignFunction { FunctionId = o.Id, IsSelect = true }).ToList()
            };
            return RoleModel;

        }

        public AssignUserModel ToAssignUserModel()
        {
            var assignUserModel = new AssignUserModel
            {
                Id= Id,
                RoleName = Name,
                Title = Title,
                Status = Status,
                Users = RoleUsers.Select(o => new Entity.AssignUser { UserId = o.Id, IsSelect = true }).ToList()
            };
            return assignUserModel;

        }
    }
}
