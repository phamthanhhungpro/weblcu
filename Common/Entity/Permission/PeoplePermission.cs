using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entity.Permission
{
    public class PeoplePermission : PermissionInfo
    {
        public bool IsViewRequest { get; set; } = false;
        public bool IsViewConfirm { get; set; } = false;
        public bool IsViewReject { get; set; } = false;
        public bool IsConfirm { get; set; } = false;
        public bool IsShow { get; set; } = false;
        public bool IsRequest { get; set; } = false;
        public bool IsReject { get; set; } = false;
    }
}
