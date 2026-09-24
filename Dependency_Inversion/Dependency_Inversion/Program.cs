using System;
using Dependency_Inversion;

public class Program
{
    public static void Main()
    {
        ServicioReportes servicio1 = new ServicioReportes(new LoggerConsola());
        servicio1.GenerarReporte();

        ServicioReportes servicio2 = new ServicioReportes(new LoggerArchivo());
        servicio2.GenerarReporte();
    }
}