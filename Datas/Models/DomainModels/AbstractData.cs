using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Datas.Models.DomainModels
{
    public abstract class AbstractData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Trạng thái")]
        public Enums.ActiveStatus Status { get; set; } = Enums.ActiveStatus.Active;

        [Display(Name = "Trạng thái xóa")]
        public Enums.DeleteStatus DeleteStatus { get; set; } = Enums.DeleteStatus.Normal;

        [Display(Name = "Ngày tạo")]
        public DateTime InsertDate { get; set; } = DateTime.Now;

        [Display(Name = "Cập nhật")]
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        public virtual bool IsExistAnother()
        {
            return false;
        }
    }
}
