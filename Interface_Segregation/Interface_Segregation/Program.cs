using System;
using Interface_Segregation;
public class Program
{
    public static void Main()
    {
        Desarrollador dev1 = new Desarrollador();
        dev1.Programar();

        DesarrolladorFullStack dev2 = new DesarrolladorFullStack();
        dev2.Programar();
        dev2.DisenarUI();
    }
}