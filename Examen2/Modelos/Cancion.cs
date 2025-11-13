namespace Examen2;
internal class Cancion
{
    public string Nombre { get; set; }
    public string Artista { get; set; }
    public int DuracionSegundos { get; set; }

    public Cancion(string nombre, string artista, int duracionSegundos)
    {
        Nombre = nombre;
        Artista = artista;
        DuracionSegundos = duracionSegundos;
    }

    public override string ToString()
    {
        int minutos = DuracionSegundos / 60;
        int segundos = DuracionSegundos % 60;

        return $"{Nombre} - {Artista} ({minutos}:{segundos:D2})";
    }

}
