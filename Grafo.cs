using System;
using System.Collections.Generic;
using System.IO;

public partial class Grafo
{
    // Diccionario principal del grafo: Ciudad -> (Destino, Costo)
    private Dictionary<string, Dictionary<string, int>> vuelos;

    public Grafo()
    {
        vuelos = new Dictionary<string, Dictionary<string, int>>();
    }

    public void CargarDesdeArchivo(string rutaArchivo)
    {
        string[] lineas = File.ReadAllLines(rutaArchivo);
        foreach (string linea in lineas)
        {
            string[] datos = linea.Split(',');
            string origen = datos[0];
            string destino = datos[1];
            int costo = int.Parse(datos[2]);

            if (!vuelos.ContainsKey(origen)) vuelos[origen] = new Dictionary<string, int>();
            if (!vuelos.ContainsKey(destino)) vuelos[destino] = new Dictionary<string, int>();

            vuelos[origen][destino] = costo;
            vuelos[destino][origen] = costo; // Asumimos vuelos ida y vuelta con mismo costo
        }
        Console.WriteLine($"Base de datos cargada con éxito. {vuelos.Count} ciudades registradas.");
    }
}