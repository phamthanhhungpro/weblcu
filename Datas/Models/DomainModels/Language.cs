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
    public class Language : AbstractData
    {

        [Required(ErrorMessage = "Bạn cần nhập tên ngôn ngữ")]
        [Display(Name = "Tên ngôn ngữ")]
        public string Name { set; get; }

        [Display(Name = "Chi tiết")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        //public virtual ICollection<Ward> Wards { set; get; }

        public virtual ICollection<People> Peoples { set; get; }

        public override bool IsExistAnother()
        {
            return Peoples.Any(o => o.DeleteStatus == Enums.DeleteStatus.Normal);
        }

        public void SetNewData(Language model)
        {
            Name = model.Name;
            Details = model.Details;
            Status = model.Status;
        }

        public void SetNewData(LanguageModel model)
        {
            Name = model.Name;
            Details = model.Details;
            Status = model.Status;
        }

        public LanguageModel ToLanguageModel()
        {
            var model = new LanguageModel
            {
                Id = Id,
                Name = Name,
                Details = Details,
                Status = Status
            };
            return model;
        }
    }
}
