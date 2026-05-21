// Programa: lee cinco números y calcula su promedio
class Program
{
    static void Main()
    {
        const int cantidad = 5;
        double suma = 0;

        for (int i = 1; i <= cantidad; i++)
        {
            double numero;
            while (true)
            {
                Console.Write($"Ingrese el número {i}: ");
                string? entrada = Console.ReadLine();
                if (double.TryParse(entrada, out numero))
                {
                    suma += numero;
                    break;
                }
                Console.WriteLine("Entrada no válida. Por favor ingrese un número.");
            }
        }

        double promedio = suma / cantidad;
        Console.WriteLine($"\nLa suma es: {suma}");
        Console.WriteLine($"El promedio de los {cantidad} números es: {promedio:F2}");
    }
}
