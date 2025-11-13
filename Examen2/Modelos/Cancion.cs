namespace Examen2;
class Usuario
{
    public string Nombre { get; set; }
    public Dictionary<string, List<Cancion>> ListasReproduccion { get; set; }

    public Usuario(string nombre)
    {
        Nombre = nombre;
        ListasReproduccion = new Dictionary<string, List<Cancion>>();
    }

    public void CrearListaReproducción(string nombre)
    {
        if (ListasReproduccion.Contains(nombre))
        {
            Console.WriteLine("La lista ya existe");
        }
        else
        {
            ListasReproduccion[nombre] = new List<Cancion>();
            Console.WriteLine("Nueva lista creada");
        }
    }

    public void AgregarCancionALista( string nombreLista, Cancion cancion)
    {
        if (ListasReproduccion.Contains(nombreLista){
            ListasReproduccion[nombreLista].Add(cancion);
            Console.WriteLine($"Canción: {cancion.Titulo}, agregada a la lista: {nombreLista}");
        }
        else
        {
            Console.WriteLine("No existe la lista");
        }
    }
    public void MostrarListasReproduccion()
    {
        if (ListasReproduccion.Count == 0)
        {
            Console.WriteLine("No tienes listas creadas aún.");
            return;
        }

        foreach (var lista in ListasReproduccion)
        {
            Console.WriteLine($"\n {lista.Key}:");
            if (lista.Value.Count == 0)
            {
                Console.WriteLine("   (Lista vacía)");
            }
            else
            {
                foreach (var cancion in lista.Value)
                {
                    Console.WriteLine($"   - {cancion}");
                }
            }

        }
    }
}
