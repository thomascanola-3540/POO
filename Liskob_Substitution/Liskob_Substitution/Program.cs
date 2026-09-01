using Liskob_Substitution;
using System;
public class Program
{
    public static void MostrarMovimiento(Ave ave)
    {
        Console.WriteLine(ave.Moverse());
    }

    public static void Main()
    {
        Ave ave1 = new Aguila { Nombre = "Águila" };
        Ave ave2 = new Pato { Nombre = "Pato" };
        Ave ave3 = new Ave { Nombre = "Ave genérica" };

        MostrarMovimiento(ave1);
        MostrarMovimiento(ave2);
        MostrarMovimiento(ave3);

    }
}