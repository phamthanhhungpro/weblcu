using Common;
using Datas.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enums;

namespace Datas.Models.DomainModels
{
    public class PeopleConfirm : AbstractData
    {
        [Display(Name = "Nội dung")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; }

        [Display(Name = "Dân tộc")]
        public int? PeopleId { set; get; }
        public virtual People People { set; get; }

        [Display(Name = "Trạng thái")]
        public PeopleConfirmStatus ConfirmStatus { set; get; } = PeopleConfirmStatus.Update;


        public void SetNewData(PeopleConfirm model)
        {
            Details = model.Details;
            Status = model.Status;
        }

        public void SetNewData(PeopleConfirmModel model)
        {
            Details = model.Details;
            Status = model.Status;
        }

        public PeopleConfirmModel ToPeopleConfirmModel()
        {
            var model = new PeopleConfirmModel
            {
                Id = Id,
                Details = Details,
                Status = Status
            };
            return model;
        }
    }
}
