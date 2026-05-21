using System;

Console.WriteLine("Programa: operaciones con tres números enteros");

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

// Leer los tres números
int a = LeerEntero("Introduce el primer número: ");
int b = LeerEntero("Introduce el segundo número: ");
int c = LeerEntero("Introduce el tercer número: ");

Console.WriteLine();

// Suma
Console.WriteLine($"{a} + {b} + {c} = {a + b + c}");

// Resta
Console.WriteLine($"{a} - {b} - {c} = {a - b - c}");

// Multiplicación
Console.WriteLine($"{a} * {b} * {c} = {a * b * c}");

// División
if (b == 0 || c == 0)
{
    Console.WriteLine("División: error (división por cero).");
}
else
{
    double resultado = (double)a / b / c;
    Console.WriteLine($"{a} / {b} / {c} = {resultado}");
}
