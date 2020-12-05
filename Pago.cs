using System;
using System.Collections.Generic;
using System.Text;

namespace LQPractica3
{
    class Pago
    {
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public float Monto { get; set; }
        public bool Procesado { get; set; }
        //Agregar en 3.5
        public int IdExternoEmpleado { get; set; }
    }
}
