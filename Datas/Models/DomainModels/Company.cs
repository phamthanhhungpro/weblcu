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
    public class Company : AbstractData
    {
        [Required(ErrorMessage = "Bạn cần nhập tên đơn vị")]
        [Display(Name = "Tên đơn vị")]
        public string Name { get; set; }

        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; } = string.Empty;

        [Display(Name = "SĐT")]
        public string? PhoneNumber { get; set; } = string.Empty;

        public int? ParentId { get; set; }
        [Display(Name = "Đơn vị cha")]
        public virtual Company Parent { get; set; }

        [Display(Name = "Đơn vị con")]
        public virtual ICollection<Company> Childrens { get; set; }

        [Display(Name = "Phòng tổ con")]
        public virtual ICollection<Department> Departments { get; set; }

        public virtual ICollection<User> CompanyUsers { get; set; }


        public void SetNewData(Company company)
        {

            Name = company.Name;
            Address = company.Address;
            PhoneNumber = company.PhoneNumber;
            Status = company.Status;
            Parent = company.Parent;
            Childrens = company.Childrens;
            Departments = company.Departments;
            CompanyUsers = company.CompanyUsers;
        }
        public void SetNewData(CompanyModel company)
        {
            Name = company.Name;
            Address = company.Address;
            PhoneNumber = company.PhoneNumber;
            Status = company.Status;
        }
        public override bool IsExistAnother()
        {
            return Childrens.Any(o => o.DeleteStatus == Enums.DeleteStatus.Normal) | CompanyUsers.Any(o => o.DeleteStatus == Enums.DeleteStatus.Normal) | Departments.Any(o => o.DeleteStatus == Enums.DeleteStatus.Normal);
        }
        public CompanyModel ToCompanyModel()
        {
            var companyModel = new CompanyModel
            {
                Id = Id,
                Address = Address,
                Name = Name,
                PhoneNumber = PhoneNumber,
                Status = Status,
            };
            if (Parent != null)
            {
                companyModel.ParentId = Parent.Id;
            }
            return companyModel;
        }
    }
}
