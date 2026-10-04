using SkiaSharp;
using System.IO;
using System.Collections.Generic;

public partial class Grafo
{
    public void Graficar()
    {
        using SKBitmap imagen = new SKBitmap(800, 600);
        using SKCanvas canvas = new SKCanvas(imagen);
        canvas.Clear(SKColors.White);

        using SKPaint linea = new SKPaint { Color = SKColors.Gray, StrokeWidth = 3, IsAntialias = true };
        using SKPaint nodo = new SKPaint { Color = SKColors.SkyBlue, IsAntialias = true };
        using SKPaint textoPaint = new SKPaint { Color = SKColors.Black, IsAntialias = true };
        using SKPaint textoCostoPaint = new SKPaint { Color = SKColors.DarkRed, IsAntialias = true };

        using SKFont fuente = new SKFont(SKTypeface.Default, 18);
        using SKFont fuenteCosto = new SKFont(SKTypeface.Default, 16);

        // Coordenadas manuales para el mapa
        var posiciones = new Dictionary<string, SKPoint>
        {
            { "Quito", new SKPoint(400, 150) },
            { "Guayaquil", new SKPoint(200, 300) },
            { "Cuenca", new SKPoint(250, 450) },
            { "Puyo", new SKPoint(600, 250) },
            { "Tena", new SKPoint(650, 150) },
            { "Bogota", new SKPoint(400, 50) },
            { "Lima", new SKPoint(200, 550) }
        };

        // Dibujar Aristas y costos
        var dibujadas = new HashSet<string>();
        foreach (var ciudad in vuelos)
        {
            foreach (var destino in ciudad.Value)
            {
                string idArista = ciudad.Key.CompareTo(destino.Key) < 0 ? $"{ciudad.Key}-{destino.Key}" : $"{destino.Key}-{ciudad.Key}";
                if (dibujadas.Contains(idArista)) continue;
                dibujadas.Add(idArista);

                var p1 = posiciones[ciudad.Key];
                var p2 = posiciones[destino.Key];
                canvas.DrawLine(p1, p2, linea);

                // Dibujar el costo en medio de la línea
                float midX = (p1.X + p2.X) / 2;
                float midY = (p1.Y + p2.Y) / 2;
                canvas.DrawText($"${destino.Value}", midX, midY - 5, fuenteCosto, textoCostoPaint);
            }
        }

        // Dibujar Nodos
        foreach (var pos in posiciones)
        {
            if (vuelos.ContainsKey(pos.Key)) // Solo si está en el archivo TXT
            {
                canvas.DrawCircle(pos.Value, 40, nodo);
                canvas.DrawText(pos.Key, pos.Value.X - 25, pos.Value.Y + 5, fuente, textoPaint);
            }
        }

        using SKImage skImage = SKImage.FromBitmap(imagen);
        using SKData data = skImage.Encode(SKEncodedImageFormat.Png, 100);
        using FileStream stream = File.OpenWrite("mapa_vuelos.png");
        data.SaveTo(stream);
    }
}