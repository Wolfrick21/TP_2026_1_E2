using Examen2.Gestores;
using Examen2.Servicios;
using Examen2.Modelos;

//1.- Inicializacion (ServicioMusica y 8 canciones)

GestorCanciones gestor = new GestorCanciones();
ServicioMusica servicio = new ServicioMusica();

gestor.AgregarCancion(new Cancion("Bohemian Rhapsody", "Queen", 354));
gestor.AgregarCancion(new Cancion("Hey Jude", "The Beatles", 711));
gestor.AgregarCancion(new Cancion("Ser Parte", "Siddhartha", 421));
gestor.AgregarCancion(new Cancion("Golden", "Harry Styles", 330));
gestor.AgregarCancion(new Cancion("Hotel California", "Eagles", 391));
gestor.AgregarCancion(new Cancion("Labios Rotos", "Zoé", 423));
gestor.AgregarCancion(new Cancion("DtMF", "Bad Bunny", 358));
gestor.AgregarCancion(new Cancion("Una cerveza", "Fuerza Regida", 437));
//2.- Registro de usuario
Console.WriteLine("Bienvenido al Sistema de Música Simple");
Console.WriteLine("--- REGISTRO DE USUARIO");
Console.Write("Por favor, ingrese su nombre de usuario: ");
string nombreUsuario = Console.ReadLine() ?? "";
Usuario usuario = servicio.RegistrarUsuario(nombreUsuario);
Console.WriteLine($"¡Bienvenido , {nombreUsuario}!");

//3.- Creacion de lista
Console.WriteLine("--- CREACIÓN DE LISTA DE REPRODUCCIÓN ---");
Console.Write("Ingrese el nombre de su primera lista de reproducción: ");
string nombreLista = Console.ReadLine() ?? "";
CrearListaReproduccion(nombreLista);
//List<Cancion> eqe = new List<Cancion>();

string listaActual = nombreLista;
bool opc = true;

//4.- Menu principal
while (true)
{
    Console.WriteLine("\n --- MENÚ PRINCIPAL ---");
    Console.WriteLine($"Usuario actual: {nombreUsuario}");
    Console.WriteLine($"Lista actual: '{listaActual}'");
    Console.WriteLine("1) Buscar canciones para agregar a mi lista");
    Console.WriteLine("2) Ver mi lista de reproducción (ordenada por duración)");
    Console.WriteLine("3) Ver todas las canciones disponibles");
    Console.WriteLine("4) Crear nueva lista de reproducción");
    Console.WriteLine("5) Cambiar de lista actual");
    Console.WriteLine("6) Salir");
    Console.Write("Seleccione una opción: ");
    string opcion = Console.ReadLine() ?? "";
    switch (opcion)
    {
        case "1":
            // Lógica para buscar canción y añadir a la lista
            Console.Write("Ingrese nombre de la canción a buscar: ");
            string cancionBusqueda = Console.ReadLine() ?? "";
            var busqueda = gestor.BuscarPorNombre(cancionBusqueda);

            if (busqueda.Count == 0)
            {
                Console.WriteLine("No se encontraron canciones con ese nombre.");
                return;
            }
            Console.WriteLine("Resultados numerados:");
            for (int i = 0; i < busqueda.Count; i++)
                Console.WriteLine($"{i + 1}. {busqueda[i]}");

            Console.Write("Seleccione el número de la canción a agregar: ");
            int opc1 = int.Parse(Console.ReadLine() ?? "");
            usuario.AgregarCancionALista(listaActual, busqueda[opc1 - 1]);
            break;
        case "2":
            // Lógica para ordenar lista por duración y mostrar duración total
            if (!usuario.ListasReproduccion.ContainsKey(listaActual))
            {
                Console.WriteLine("No existe la lista actual.");
                return;
            }

            var lista = new List<Cancion>(usuario.ListasReproduccion[listaActual]);

            if (lista.Count == 0)
            {
                Console.WriteLine("La lista está vacía.");
                return;
            }

            gestor.QuickSort(lista, 0, lista.Count - 1);
            Console.WriteLine($"\nLista \"{listaActual}\" ordenada por duración:");
            CalcularDuracionTotal(lista);

            // ===== Función auxiliar =====
            static void CalcularDuracionTotal(IEnumerable<Cancion> canciones)
            {
                int totalSegundos = canciones.Sum(c => c.DuracionSegundos);
                Console.WriteLine($"Duración total: {totalSegundos / 60}m {totalSegundos % 60:D2}s");
            }
            break;
        case "3":
            // Lógica para mostrar canciones disponibles
            Console.WriteLine("Canciones disponibles:");
            gestor.MostrarCancionesDisponibles();
            break;
        case "4":
            // Lógica para crear nueva lista de reproducción
            Console.Write("Ingrese el nombre de la nueva lista: ");
            string nuevoNombre = Console.ReadLine();
            usuario.CrearListaReproduccion(nuevoNombre);
            listaActual = nuevoNombre;
            break;
        case "5":
            // Lógica para cambiar lista actual / mostrar listas    
            Console.WriteLine("Listas disponibles:");
            var listas = usuario.ListasReproduccion.Keys.ToList();
            for (int i = 0; i < listas.Count; i++)
                Console.WriteLine($"{i + 1}. {listas[i]}");

            Console.Write("Seleccione el número de la lista que desea colocar como actual: ");
            int opc2 = int.Parse(Console.ReadLine() ?? "");

            if (opc2 > 0 && opc2 <= listas.Count)
            {
                listaActual = listas[opc2 - 1];
                List<Cancion> listaSeleccionada = usuario.ListasReproduccion[listaActual];
                Console.WriteLine($"Lista actual cambiada a: {listaActual} ({listaSeleccionada.Count})");
                Console.WriteLine("Canciones en esta lista:");
                foreach (var c in listaSeleccionada)
                    Console.WriteLine(c.ToString());
            }
            else
            {
                Console.WriteLine("Selección inválida.");
            }
            break;
        case "6":
            opc = false;
            Console.WriteLine("Saliendo.....");
            break;
        default:
            Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
            break;
    }
}
