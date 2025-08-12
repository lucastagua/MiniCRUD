using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCRUD.Models
{
    public class Contact
    {
        public int IdContact { get; set; }
        public string Names { get; set; }
        public string surnames { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }

    }
}