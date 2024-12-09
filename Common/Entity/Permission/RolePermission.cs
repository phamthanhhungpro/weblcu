using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entity.Permission
{
    public class RolePermission : PermissionInfo
    {
        public bool isAssign { set; get; } = false;
    }
}
