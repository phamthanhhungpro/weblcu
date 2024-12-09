using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public abstract class AbstractData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Trạng thái")]
        public Enums.ActiveStatus Status { get; set; } = Enums.ActiveStatus.Active;

        [Display(Name = "Ngày tạo")]
        public DateTime InsertDate { get; set; } = DateTime.Now;
    }
}
