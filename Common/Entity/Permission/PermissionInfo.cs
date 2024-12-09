using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entity.Permission
{
    public class PermissionInfo
    {
        public bool IsView { get; set; } = false;
        public bool IsAdd { get; set; } = false;
        public bool IsEdit { get; set; } = false;
        public bool IsDelete { get; set; } = false;

        public void SetValue(bool value)
        {
            IsView = IsAdd = IsEdit = IsDelete = value;
        }
    }
}
