using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Elige qué programa ejecutar:");
        Console.WriteLine("1. Paréntesis Balanceados");
        Console.WriteLine("2. Torres de Hanoi");
        Console.Write("Opción: ");
        string opcion = Console.ReadLine();

        if (opcion == "1")
        {
            EjecutarParentesisBalanceados();
        }
        else if (opcion == "2")
        {
            EjecutarTorresDeHanoi();
        }
        else
        {
            Console.WriteLine("Opción inválida.");
        }
    }

    // --- Programa Paréntesis Balanceados ---
    static void EjecutarParentesisBalanceados()
    {
        string entrada = "{7 + (8 * 5) - [(9 - 7) + (4 + 1)]}";
        bool balanceado = EstaBalanceada(entrada);

        if (balanceado)
            Console.WriteLine("Fórmula balanceada.");
        else
            Console.WriteLine("Fórmula no balanceada.");
    }

    static bool EstaBalanceada(string expresion)
    {
        Stack<char> pila = new Stack<char>();

        foreach (char caracter in expresion)
        {
            if (caracter == '(' || caracter == '{' || caracter == '[')
            {
                pila.Push(caracter);
            }
            else if (caracter == ')' || caracter == '}' || caracter == ']')
            {
                if (pila.Count == 0)
                    return false;

                char ultimo = pila.Pop();

                if (!EsPar(ultimo, caracter))
                    return false;
            }
        }
        return pila.Count == 0;
    }

    static bool EsPar(char apertura, char cierre)
    {
        return (apertura == '(' && cierre == ')') ||
               (apertura == '{' && cierre == '}') ||
               (apertura == '[' && cierre == ']');
    }

    // --- Programa Torres de Hanoi ---
    static void EjecutarTorresDeHanoi()
    {
        int numDiscos = 3;

        Torre origen = new Torre("Origen");
        Torre auxiliar = new Torre("Auxiliar");
        Torre destino = new Torre("Destino");

        for (int i = numDiscos; i >= 1; i--)
            origen.Apilar(i);

        Console.WriteLine("Estado inicial:");
        MostrarEstado(origen, destino, auxiliar);

        MoverDiscos(numDiscos, origen, destino, auxiliar);

        Console.WriteLine("¡Tarea completada!");
    }

    class Torre
    {
        public Stack<int> Discos { get; private set; } = new Stack<int>();
        public string Nombre { get; private set; }

        public Torre(string nombre)
        {
            Nombre = nombre;
        }

        public void Apilar(int disco)
        {
            Discos.Push(disco);
        }

        public int Desapilar()
        {
            return Discos.Pop();
        }

        public void Mostrar()
        {
            Console.Write($"{Nombre}: ");
            foreach (var d in Discos)
                Console.Write(d + " ");
            Console.WriteLine();
        }
    }

    static void MoverDiscos(int n, Torre origen, Torre destino, Torre auxiliar)
    {
        if (n == 1)
        {
            int disco = origen.Desapilar();
            destino.Apilar(disco);
            Console.WriteLine($"Mover disco {disco} de {origen.Nombre} a {destino.Nombre}");
            MostrarEstado(origen, destino, auxiliar);
        }
        else
        {
            MoverDiscos(n - 1, origen, auxiliar, destino);
            MoverDiscos(1, origen, destino, auxiliar);
            MoverDiscos(n - 1, auxiliar, destino, origen);
        }
    }

    static void MostrarEstado(Torre t1, Torre t2, Torre t3)
    {
        t1.Mostrar();
        t2.Mostrar();
        t3.Mostrar();
        Console.WriteLine();
    }
}

