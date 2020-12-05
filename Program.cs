using System;
using System.Collections.Generic;
using System.Linq;

namespace LQPractica3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Empleado> empleados = new List<Empleado>()
            {
                new Empleado {
                    Nombre = "Daniela",
                    Apellido = "Pérez",//5
                    Departamento = Departamento.Desarrollo,
                    Edad = 29,
                    IdExterno = 1 //agregar en 3.5
                },
                new Empleado {
                    Nombre = "José",
                    Apellido = "Lima Rico",//9
                    Departamento = Departamento.Admin,
                    Edad = 40,
                    IdExterno = 2 //agregar en 3.5
                },
                 new Empleado {
                    Nombre = "Fernanda",
                    Apellido = "Vega Valle",//10
                    Departamento = Departamento.Desarrollo,
                    Edad = 35,
                    IdExterno = 3 //agregar en 3.5
                },
                  new Empleado {
                    Nombre = "Fabiola",
                    Apellido = "Cortés Vázquez",//14
                    Departamento = Departamento.Desarrollo,
                    Edad = 25,
                    IdExterno = 4,//agregar en 3.5
                    Pagos = new List<Pago>
                    {
                        new Pago
                        {
                            Descripcion = "Quincena #1: Diciembre",
                            Fecha = new DateTime(2020,12,15),
                            Monto = 15000.95f,
                            Procesado = true,
                        },
                    }
                },
                   new Empleado {
                    Nombre = "Mónica",
                    Apellido = "Correa",//6
                    Departamento = Departamento.Soporte,
                    Edad = 22,
                    IdExterno = 5, //agregar en 3.5
                    Pagos = new List<Pago>
                    {
                        new Pago
                        {
                            Descripcion = "Quincena #21: Noviembre",
                            Fecha = new DateTime(2020,11,15),
                            Monto = 18000.95f,
                            Procesado = true,
                        },
                        new Pago
                        {
                            Descripcion = "Quincena #22: Noviembre",
                            Fecha = new DateTime(2020,11,30),
                            Monto = 20000.95f,
                            Procesado = false,
                        }
                    }
                },
            };

            #region filtrar, ordenar, agrupar
            var filtroReversa = empleados
                .Where(u => u.Edad <= 30) //filtrar
                .Reverse(); //ordenar
            ImprimeEmpleados(filtroReversa, "/** Reverse ** /");

            //otros filtrados - Skip
            var fs = filtroReversa.Skip(1);
            ImprimeEmpleados(fs, "/** Skip ** /");

            //otros filtrados - TakeWhile
            var fsw = filtroReversa.TakeWhile(e => e.Edad <= 25);
            ImprimeEmpleados(fsw, "/** TakeWhile ** /");

            //filtrar + ordenar + agrupar
            var filtroAgrupado = empleados
             .Where(u => u.Edad <= 30) //filtrar
             .OrderBy(uo => uo.Nombre) //OrderByDescending //ordenar
             .GroupBy(ug => ug.Departamento); //agrupar
            #endregion

            #region proyeccion y cuantificacion
            //proyeccion - Select
            var pagos = empleados
                .Where(u => u.Departamento == Departamento.Desarrollo)
                .Select(us => us.Pagos);
            //proyeccion - SelectMany
            var pagosB = empleados
                .Where(u => u.Departamento == Departamento.Desarrollo 
                    &&  u.Pagos != null)
                .SelectMany(us => us.Pagos);

            var pagosEmpleados = empleados
                .Where(e => e.Pagos != null)
                .SelectMany(e => e.Pagos, (e, pago) => new { 
                                        e.Nombre, 
                                        pago.Descripcion,
                                        pago.Monto
                                      });
            //cuantificadores - Any
            var monto = 20000f;
            var existeUnPagoMayor = pagosEmpleados.Any(p => p.Monto >= monto);
            //cuantificadores - All
            var todosPagosMayores = pagosEmpleados.All(p => p.Monto >= monto);
            #endregion

            #region agregacion y conversion
            //agregacion - Count
            var cantidadPagos = pagosEmpleados.Count();
            Console.WriteLine($"Pagos {cantidadPagos}");
            //agregacion - Average
            var pagoProm = pagosEmpleados.Average(p => p.Monto);
            Console.WriteLine($"Promedio de monto {pagoProm}");
            //agregacion - Max
            var pagoMin = pagosEmpleados.Min(p => p.Monto);
            Console.WriteLine($"Pago minimo {pagoMin}");

            //conversion - ToArray
            var arr = pagosEmpleados.ToArray();
            //conversion - To List
            var ls = pagosEmpleados.ToList();
            #endregion

            #region element
            //First
            //var ppago = pagosEmpleados.First(pp => pp.Monto < 500);
            //FirstOrDefault
            var ppagob = pagosEmpleados.FirstOrDefault(pp => pp.Monto < 500);
            //SingleOrDefault
            var unpago = pagosEmpleados.SingleOrDefault(up => up.Monto == 20000.95f);//29000.95f
            Console.WriteLine(unpago);
            //ElementAtOrDefault
            var elpago = pagosEmpleados.ElementAtOrDefault(0);
            Console.WriteLine(elpago);
            #endregion

            #region join
            var nPagos = new List<Pago>
            {
                new Pago
                {
                    Descripcion = "Quincena Junio",
                    Fecha = new DateTime(2020,06,15),
                    Monto = 12000.95f,
                    Procesado = true,
                    IdExternoEmpleado = 2
                },
                new Pago
                {
                    Descripcion = "Quincena Septiembre",
                    Fecha = new DateTime(2020,06,30),
                    Monto = 22000.95f,
                    Procesado = false,
                    IdExternoEmpleado = 3
                },
                new Pago
                {
                    Descripcion = "Bono Diciembre",
                    Fecha = new DateTime(2020,12,15),
                    Monto = 50000.00f,
                    Procesado = true,
                    IdExternoEmpleado = 3
                }
            };
            //Join
            var empleadosPagos = empleados.Join(nPagos,
                                    emp => emp.IdExterno,
                                    pago => pago.IdExternoEmpleado,
                                    (emp, pago) => new
                                    {
                                        emp.Nombre,
                                        pago.Monto
                                    });
            //GroupJoin
            var empleadosPagosGroup = empleados.GroupJoin(nPagos,
                                    emp => emp.IdExterno,
                                    pago => pago.IdExternoEmpleado,
                                    (emp, pagos) => new
                                    {
                                        Empleado = emp.Nombre,
                                        PagosAgregados = pagos,
                                    });
            foreach (var e in empleadosPagosGroup)
            {
                if(e.PagosAgregados.Count() > 0 )
                    Console.WriteLine(e.Empleado);

                foreach (var p in e.PagosAgregados)
                    Console.WriteLine(p.Monto);
            }

            #endregion
        }

        static void ImprimeEmpleado(Empleado e)
        {
            string fila = string.Format("{0,-40} {1,-10} {2,-20} {3,-10} {4}",
                    e.Id, e.Nombre, e.Apellido, e.Edad, e.Departamento);
            Console.WriteLine(fila);
        }

        static void ImprimeEmpleados(IEnumerable<Empleado> lista, string titulo="/** --- ** /")
        {
            Console.WriteLine(titulo);
            var encabezado = string.Format("{0,-40} {1,-10} {2,-20} {3,-10} {4}",
                           "ID", "Nombre", "Apellido", "Edad", "Departamento");
            Console.WriteLine(encabezado);
            foreach (var el in lista)
            {
                ImprimeEmpleado(el);
            }
        }
        
    }
}
