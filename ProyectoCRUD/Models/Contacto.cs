﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoCRUD.Models
{
    public class Contacto
    {
        public int IdContacto { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Teléfono { get; set; }
        public string Correo { get; set; }

    }
}