using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entity.Permission
{
    public class NewsPermission : PermissionInfo
    {
        public bool IsPost { get; set; } = false;
    }
}
