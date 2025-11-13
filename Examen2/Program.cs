using Examen2.Gestores;
using Examen2.Servicios;
using Examen2.Modelos;

// 1.- Inicialización (ServicioMusica y 8 canciones)
GestorCanciones gestor = new GestorCanciones();
ServicioMusica servicio = new ServicioMusica();

gestor.AgregarCancion(new Cancion("Bohemian Rhapsody", "Queen", 354));
gestor.AgregarCancion(new Cancion("Hey Jude", "The Beatles", 431));
gestor.AgregarCancion(new Cancion("Ser Parte", "Siddhartha", 241));
gestor.AgregarCancion(new Cancion("Golden", "Harry Styles", 210));
gestor.AgregarCancion(new Cancion("Hotel California", "Eagles", 391));
gestor.AgregarCancion(new Cancion("Labios Rotos", "Zoé", 263));
gestor.AgregarCancion(new Cancion("DTMF", "Bad Bunny", 198));
gestor.AgregarCancion(new Cancion("Una Cerveza", "Fuerza Regida", 254));

// 2.- Registro de usuario
Console.WriteLine("Bienvenido al Sistema de Música Simple");
Console.WriteLine("--- REGISTRO DE USUARIO ---");
Console.Write("Por favor, ingrese su nombre de usuario: ");
string nombreUsuario = Console.ReadLine() ?? "";
servicio.RegistrarUsuario(nombreUsuario);
Console.WriteLine("¡Bienvenido, " + nombreUsuario + "!");

// 3.- Creación de lista
Console.WriteLine("\n--- CREACIÓN DE LISTA DE REPRODUCCIÓN ---");
Console.Write("Ingrese el nombre de su primera lista de reproducción: ");
string nombreLista = Console.ReadLine() ?? "";

Usuario usuario = servicio.BuscarUsuario(nombreUsuario);
usuario.CrearListaReproduccion(nombreLista);

string listaActual = nombreLista;
bool ejecutando = true;

// 4.- Menú principal
while (ejecutando)
{
    Console.WriteLine("\n --- MENÚ PRINCIPAL ---");
    Console.WriteLine("Usuario actual: " + nombreUsuario);
    Console.WriteLine("Lista actual: '" + listaActual + "'");
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
            Console.Write("Ingrese nombre (o parte) de la canción a buscar: ");
            string cancionBusqueda = Console.ReadLine() ?? "";

            var resultado = gestor.BuscarPorNombre(cancionBusqueda);
            var coincidencias = resultado.coincidencias;
            int iteraciones = resultado.iteraciones;

            if (coincidencias.Count == 0)
            {
                Console.WriteLine("No se encontraron canciones con ese nombre.");
                break;
            }

            Console.WriteLine("\nResultados encontrados:");
            for (int i = 0; i < coincidencias.Count; i++)
                Console.WriteLine((i + 1) + ". " + coincidencias[i]);

            Console.Write("Seleccione el número de la canción a agregar: ");
            int seleccion;
            if (int.TryParse(Console.ReadLine(), out seleccion) && seleccion > 0 && seleccion <= coincidencias.Count)
            {
                usuario.AgregarCancionALista(listaActual, coincidencias[seleccion - 1]);
            }
            else
            {
                Console.WriteLine("Selección inválida.");
            }
            break;

        case "2":
            if (!usuario.ListasReproduccion.ContainsKey(listaActual))
            {
                Console.WriteLine("No existe la lista actual.");
                break;
            }

            var lista = new List<Cancion>(usuario.ListasReproduccion[listaActual]);

            if (lista.Count == 0)
            {
                Console.WriteLine("La lista está vacía.");
                break;
            }

            GestorCanciones.QuickSortPorDuracion(lista);
            Console.WriteLine("\nLista '" + listaActual + "' ordenada por duración:");
            foreach (var c in lista)
                Console.WriteLine(c.ToString());

            int totalSegundos = 0;
            foreach (var c in lista)
                totalSegundos += c.DuracionSegundos;

            Console.WriteLine("Duración total: " + (totalSegundos / 60) + "m " + (totalSegundos % 60).ToString("D2") + "s");
            break;

        case "3":
            gestor.MostrarCancionesDisponibles();
            break;

        case "4":
            Console.Write("Ingrese el nombre de la nueva lista: ");
            string nuevoNombre = Console.ReadLine() ?? "";
            usuario.CrearListaReproduccion(nuevoNombre);
            listaActual = nuevoNombre;
            break;

        case "5":
            if (usuario.ListasReproduccion.Count == 0)
            {
                Console.WriteLine("No tienes listas disponibles.");
                break;
            }

            Console.WriteLine("Listas disponibles:");
            var nombresListas = new List<string>(usuario.ListasReproduccion.Keys);
            for (int i = 0; i < nombresListas.Count; i++)
                Console.WriteLine((i + 1) + ". " + nombresListas[i]);

            Console.Write("Seleccione la lista a usar: ");
            int indice;
            if (int.TryParse(Console.ReadLine(), out indice) && indice > 0 && indice <= nombresListas.Count)
            {
                listaActual = nombresListas[indice - 1];
                Console.WriteLine("Lista actual cambiada a: " + listaActual);
            }
            else
            {
                Console.WriteLine("Selección inválida.");
            }
            break;

        case "6":
            Console.WriteLine("Saliendo del sistema...");
            ejecutando = false;
            break;

        default:
            Console.WriteLine("Opción no válida. Intente de nuevo.");
            break;
    }
}
