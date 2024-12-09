﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entity
{
    public class MailForgot
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}
