using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entity
{
    public class MenuItem
    {
        public string Area { get; set; } = string.Empty;
        public string Name { get; set; }

        public string Url { get; set; } = "#";

        public string ClassCss { get; set; } = "fa fa-university";

        public bool IsOpen { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public int? Notify { set; get; }

        public List<MenuItem> Childrens { get; set; } = new List<MenuItem>();

        public bool IsArea()
        {
            return !string.IsNullOrEmpty(Area);
        }
    }
}
