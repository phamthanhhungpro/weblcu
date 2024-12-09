using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.ViewModels
{
    public class AdminSettingModel
    {
        [Display(Name = "Danh sách Ip")]
        public List<string> BlockIps { get; set; } = new List<string>();

        [Display(Name = "Thời gian bắt đầu")]
        public DateTime? LockStartTime { get; set; }

        [Display(Name = "Thời gian kết thúc")]
        public DateTime? LockEndTime { get; set; }

        [Display(Name = "Kiểu khóa")]
        public int? LockTimeType { get; set; }
    }
}
