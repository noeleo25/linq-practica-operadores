using System;
using System.Collections.Generic;
using System.Text;

namespace LQPractica3
{
    public enum Departamento
    {
        RH = 201,
        Desarrollo = 520,
        Soporte = 402,
        Admin = 309
    }
    class Empleado
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Departamento Departamento { get; set; }
        public int Edad { get; set; }
        //para 3.2
        public List<Pago> Pagos { get; set; } 
        //para 3.5
        public int IdExterno { get; set; }
    }
}
