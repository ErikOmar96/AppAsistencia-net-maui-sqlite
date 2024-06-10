﻿using SQLite;

namespace AppAsistencia.Modelos
{
    public class Usuario
    {
        // Establecer IdUsuario como primary key
        [PrimaryKey, AutoIncrement]
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ClaveUsuario { get; set; }
        public string? CargoUsuario { get; set; }
        public string CorreoUsuario { get; set; }
    }
}
