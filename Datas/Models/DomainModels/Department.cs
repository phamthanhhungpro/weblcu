using Common;
using Datas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.DomainModels
{
    public class Department : AbstractData
    {

        [Display(Name = "Tên phòng tổ")]
        [Required(ErrorMessage = "Tên phòng tổ chưa được nhập")]
        public string Name { get; set; }

        [Display(Name = "Ghi chú")]
        public string? Title { get; set; } = string.Empty;

        [Display(Name = "Đơn vị")]
        public int? DepartmentCompanyId { get; set; }

        [Display(Name = "Đơn vị")]
        public virtual Company DepartmentCompany { get; set; }

        [Display(Name = "Phòng tổ cha")]
        public int? ParentId { get; set; }
        public virtual Department Parent { get; set; }

        [Display(Name = "Phòng tổ con")]
        public virtual ICollection<Department> Childrens { get; set; }

        public virtual ICollection<User> DepartmentUsers { get; set; }


        public override bool IsExistAnother()
        {
            return Childrens.Any(o => o.DeleteStatus == Enums.DeleteStatus.Normal) | DepartmentUsers.Any(o => o.DeleteStatus == Enums.DeleteStatus.Normal);
        }

        public void SetNewData(DepartmentModel model)
        {
            Name = model.Name;
            Title = model.Title;
            Status = model.Status;
        }

        public void SetNewData(Department model)
        {
            Name = model.Name;
            Title = model.Title;
            Status = model.Status;
        }
        public DepartmentModel ToDepartmentModel()
        {
            var departmentModel = new DepartmentModel
            {
                Id = Id,
                Title = Title,
                Name = Name,
                Status = Status,
            };
            if (DepartmentCompany != null)
            {
                departmentModel.CompanyId = DepartmentCompany.Id;
            }
            if (Parent != null)
            {
                departmentModel.ParentId = Parent.Id;
            }
            return departmentModel;
        }
    }
}
