using System;

Console.WriteLine("Programa: operaciones con dos números enteros");

int LeerEntero(string mensaje)
{
    while (true)
    {
        Console.Write(mensaje);
        string? entrada = Console.ReadLine();
        if (int.TryParse(entrada, out int valor))
            return valor;
        Console.WriteLine("Entrada no válida. Introduzca un entero.");
    }
}

int a = LeerEntero("Introduce el primer número: ");
int b = LeerEntero("Introduce el segundo número: ");

Console.WriteLine();
Console.WriteLine($"{a} + {b} = {a + b}");
Console.WriteLine($"{a} - {b} = {a - b}");
Console.WriteLine($"{a} * {b} = {a * b}");
if (b == 0)
{
    Console.WriteLine("División: error (división por cero).");
}
else
{
    double resultado = (double)a / b;
    Console.WriteLine($"{a} / {b} = {resultado}");
}
