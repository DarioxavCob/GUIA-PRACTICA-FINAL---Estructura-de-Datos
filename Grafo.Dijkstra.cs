using System;
using System.Collections.Generic;

public partial class Grafo
{
    public void RutaMasBarata(string origen, string destino)
    {
        if (!vuelos.ContainsKey(origen) || !vuelos.ContainsKey(destino))
        {
            Console.WriteLine("Ciudad no encontrada en el sistema.");
            return;
        }

        var distancias = new Dictionary<string, int>();
        var previos = new Dictionary<string, string>();
        var cola = new PriorityQueue<string, int>();

        foreach (var ciudad in vuelos.Keys)
        {
            distancias[ciudad] = int.MaxValue;
        }
        
        distancias[origen] = 0;
        cola.Enqueue(origen, 0);

        while (cola.Count > 0)
        {
            string actual = cola.Dequeue();

            if (actual == destino) break;

            foreach (var vecino in vuelos[actual])
            {
                int costoNuevo = distancias[actual] + vecino.Value;
                if (costoNuevo < distancias[vecino.Key])
                {
                    distancias[vecino.Key] = costoNuevo;
                    previos[vecino.Key] = actual;
                    cola.Enqueue(vecino.Key, costoNuevo);
                }
            }
        }

        // Reconstruir la ruta
        if (distancias[destino] == int.MaxValue)
        {
            Console.WriteLine($"No hay ruta disponible entre {origen} y {destino}");
            return;
        }

        List<string> ruta = new List<string>();
        string paso = destino;
        while (paso != null)
        {
            ruta.Insert(0, paso);
            previos.TryGetValue(paso, out paso);
        }

        Console.WriteLine($"\n✅ VUELO ENCONTRADO:");
        Console.WriteLine($"Ruta óptima: {string.Join(" -> ", ruta)}");
        Console.WriteLine($"Costo Total: ${distancias[destino]}");
    }
}