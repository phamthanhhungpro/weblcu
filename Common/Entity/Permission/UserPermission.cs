using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entity.Permission
{
    public class UserPermission : PermissionInfo
    {
        public bool IsLockUser { get; set; } = false;

        public bool IsResetPass { get; set; } = false;
    }
}
