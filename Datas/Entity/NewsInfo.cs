using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Entity
{
    public class NewsInfo
    {
        public int Month { get; set; }

        public bool isDisplay = false;
        public string? Title { get; set; }
        public DateTime PostDate { get; set; } = DateTime.Now;
        public string? Calendar { set; get; }
        public string? Url { set; get; }
        public string? Image { set; get; }

        public string? Details { set; get; }
    }
}
