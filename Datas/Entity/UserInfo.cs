using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Entity
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public bool SuperAdmin { set; get; } = false;
        public int? CompanyId { set; get; }
        public string CompanyName { set; get; }
        public int? ParentCompanyId { set; get; }
    }
}
