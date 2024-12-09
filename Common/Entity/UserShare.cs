using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entity
{
    public class UserShare
    {
        public int key { set; get; }

        public string title { set; get; }

        public bool expanded { set; get; } = true;

        public bool checkbox { set; get; } = false;

        public bool selected { set; get; } = false;

        public bool unselectable { set; get; } = false;

        public List<UserShare> children { set; get; }
    }
}
