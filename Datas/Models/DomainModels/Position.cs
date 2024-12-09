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
    public class Position : AbstractData
    {
        [Required(ErrorMessage = "Cần nhập tên chức vụ")]
        [Display(Name = "Tên chức vụ")]
        public string Name { set; get; }

        [Display(Name = "Chi tiết chức vụ")]
        [DataType(DataType.MultilineText)]
        public string? Details { set; get; } = string.Empty;

        [Display(Name = "Người dùng thuộc chức vụ")]
        public virtual ICollection<User> UserPosition { set; get; }

        public void SetNewData(Position model)
        {
            Name = model.Name;
            Details = model.Details;
            Status = model.Status;
            UserPosition = model.UserPosition;
        }

        public void SetNewData(PositionModel model)
        {
            Name = model.Name;
            Details = model.Details;
            Status = model.Status;
        }

        public override bool IsExistAnother()
        {
            return UserPosition.Any(o => o.DeleteStatus == Enums.DeleteStatus.Normal);
        }

        public PositionModel ToPositionModel()
        {
            var positionModel = new PositionModel
            {
                Id = Id,
                Name = Name,
                Status = Status
            };
            return positionModel;
        }
    }
}
