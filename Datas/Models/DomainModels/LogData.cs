using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Models.DomainModels
{
    public class LogData
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Display(Name = "Mã")]
        public int UserId { get; set; }

        [Display(Name = "Tài khoản")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "Họ tên")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "Method")]
        public string Method { get; set; } = string.Empty;

        [Display(Name = "Controller")]
        public string? Controller { get; set; }

        [Display(Name = "Action")]
        public string? Action { get; set; }

        [Display(Name = "Data")]
        public string? Query { get; set; }

        public string? Ip { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime InsertDate { get; set; } = DateTime.Now;

        public bool IsExistAnother()
        {
            return false;
        }
    }
}
