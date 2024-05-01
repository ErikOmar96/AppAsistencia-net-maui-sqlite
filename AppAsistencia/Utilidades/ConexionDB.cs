using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAsistencia.Utilidades
{
    public static class ConexionDB
    {
        // Retornar la ruta de la base de datos
        public static string DevolverRuta(string nombreBaseDatos)
        {
            // Cadena de ruta de la base de datos
            string rutaBaseDatos = string.Empty;

            // Validar si estamos usando Android
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                rutaBaseDatos = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                rutaBaseDatos = Path.Combine(rutaBaseDatos, nombreBaseDatos);
            }

            return rutaBaseDatos;
        }
    }
}
