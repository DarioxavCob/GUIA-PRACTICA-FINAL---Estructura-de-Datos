using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== SISTEMA DE VUELOS BARATOS UEA ===");
        Grafo sistemaVuelos = new Grafo();

        // 1. Cargar base de datos ficticia desde el bloque de notas
        sistemaVuelos.CargarDesdeArchivo("vuelos.txt");

        // 2. Buscar el vuelo más barato (Ejemplo: Guayaquil a Puyo)
        string origen = "Guayaquil";
        string destino = "Puyo";
        sistemaVuelos.RutaMasBarata(origen, destino);

        // 3. Generar la gráfica del grafo
        sistemaVuelos.Graficar();
        Console.WriteLine("\nEl mapa de vuelos ha sido generado en 'mapa_vuelos.png'.");
    }
}