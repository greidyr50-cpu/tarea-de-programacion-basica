using System.Globalization;

class Program
{
    static void Main()
    {
        Console.Write("Cantidad: ");
        var cantidadInput = Console.ReadLine();
        if (!int.TryParse(cantidadInput, out int cantidad) || cantidad < 0)
        {
            Console.WriteLine("Cantidad inválida.");
            return;
        }

        Console.Write("Precio unitario: ");
        var precioInput = Console.ReadLine();
        if (precioInput == null)
        {
            Console.WriteLine("Precio inválido.");
            return;
        }

        // Aceptar coma o punto como separador decimal
        precioInput = precioInput.Trim().Replace(',', '.');

        if (!decimal.TryParse(precioInput, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal precio) || precio < 0)
        {
            Console.WriteLine("Precio inválido.");
            return;
        }

        decimal importe = cantidad * precio;

        Console.WriteLine($"Importe: {importe.ToString("C2", CultureInfo.CurrentCulture)}");
    }
}
