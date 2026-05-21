using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        Console.WriteLine("Calcular sueldo neto (AFP 2.87%, SFS 3.04%)");

        decimal pago;
        while (true)
        {
            Console.Write("Ingrese pago por hora (pago por hora): ");
            var entrada = Console.ReadLine();
            if (decimal.TryParse(entrada, NumberStyles.Number, CultureInfo.CurrentCulture, out pago) ||
                decimal.TryParse(entrada, NumberStyles.Number, CultureInfo.InvariantCulture, out pago))
                break;
            Console.WriteLine("Valor no válido. Intente de nuevo.");
        }

        decimal horas;
        while (true)
        {
            Console.Write("Ingrese horas trabajadas: ");
            var entrada = Console.ReadLine();
            if (decimal.TryParse(entrada, NumberStyles.Number, CultureInfo.CurrentCulture, out horas) ||
                decimal.TryParse(entrada, NumberStyles.Number, CultureInfo.InvariantCulture, out horas))
                break;
            Console.WriteLine("Valor no válido. Intente de nuevo.");
        }

        decimal sueldoBruto = pago * horas;
        decimal afp = Math.Round(sueldoBruto * 0.0287m, 2);
        decimal sfs = Math.Round(sueldoBruto * 0.0304m, 2);
        decimal totalDescuentos = afp + sfs;
        decimal sueldoNeto = sueldoBruto - totalDescuentos;

        Console.WriteLine();
        Console.WriteLine($"Sueldo bruto: {sueldoBruto.ToString("C2", CultureInfo.CurrentCulture)}");
        Console.WriteLine($"AFP (2.87%): {afp.ToString("C2", CultureInfo.CurrentCulture)}");
        Console.WriteLine($"SFS (3.04%): {sfs.ToString("C2", CultureInfo.CurrentCulture)}");
        Console.WriteLine($"Total descuentos: {totalDescuentos.ToString("C2", CultureInfo.CurrentCulture)}");
        Console.WriteLine($"Sueldo neto: {sueldoNeto.ToString("C2", CultureInfo.CurrentCulture)}");

        Console.WriteLine();
        Console.WriteLine("Presione Enter para salir...");
        Console.ReadLine();
    }
}
