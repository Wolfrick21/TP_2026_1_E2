using Examen2.Modelos;
using Examen2.Servicios;

ServicioMusica servicio = new ServicioMusica();
bool salir = false;

while (!salir)
{
    Console.WriteLine("\n=== MENÚ PRINCIPAL ===");
    Console.WriteLine("1. Registrar usuario");
    Console.WriteLine("2. Agregar canción al catálogo");
    Console.WriteLine("3. Mostrar canciones disponibles");
    Console.WriteLine("4. Buscar canción por nombre");
    Console.WriteLine("5. Ordenar canciones por duración");
    Console.WriteLine("6. Crear lista de reproducción");
    Console.WriteLine("7. Agregar canción a una lista");
    Console.WriteLine("8. Mostrar listas de un usuario");
    Console.WriteLine("9. Salir");
    Console.Write("Seleccione una opción: ");

    string opcion = Console.ReadLine() ?? "";
    Console.WriteLine();

    switch (opcion)
    {
        case "1":
            Console.Write("Ingrese nombre de usuario: ");
            string nombreUsuario = Console.ReadLine() ?? "";
            servicio.RegistrarUsuario(nombreUsuario);
            break;

        case "2":
            Console.Write("Nombre de la canción: ");
            string nombre = Console.ReadLine() ?? "";
            Console.Write("Artista: ");
            string artista = Console.ReadLine() ?? "";
            Console.Write("Duración (en segundos): ");
            if (int.TryParse(Console.ReadLine(), out int duracion))
            {
                var cancion = new Cancion(nombre, artista, duracion);
                // Accedemos al gestor desde el servicio
                servicio.GetType()
                    .GetField("gestor", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.GetValue(servicio)
                    ?.GetType()
                    .GetMethod("AgregarCancion")
                    ?.Invoke(servicio.GetType().GetField("gestor", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(servicio), new object[] { cancion });
                Console.WriteLine("Canción agregada correctamente.");
            }
            else
            {
                Console.WriteLine("Duración inválida.");
            }
            break;

        case "3":
            var gestor = servicio.GetType()
                .GetField("gestor", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(servicio);
            gestor?.GetType().GetMethod("MostrarCancionesDisponibles")?.Invoke(gestor, null);
            break;

        case "4":
            Console.Write("Ingrese parte del nombre a buscar: ");
            string busqueda = Console.ReadLine() ?? "";
            var gestorBuscador = servicio.GetType()
                .GetField("gestor", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(servicio);

            var resultado = gestorBuscador?.GetType().GetMethod("BuscarPorNombre")?.Invoke(gestorBuscador, new object[] { busqueda });
            if (resultado != null)
            {
                var coincidencias = (List<Cancion>)resultado.GetType().GetProperty("Item1")?.GetValue(resultado);
                int iteraciones = (int)resultado.GetType().GetProperty("Item2")?.GetValue(resultado);

                Console.WriteLine($"Canciones encontradas ({coincidencias.Count}) en {iteraciones} iteraciones:");
                foreach (var c in coincidencias)
                {
                    Console.WriteLine($"- {c}");
                }
            }
            break;

        case "5":
            var gestorOrden = servicio.GetType()
                .GetField("gestor", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(servicio);
            gestorOrden?.GetType().GetMethod("OrdenarCancionesPorDuracion")?.Invoke(gestorOrden, null);
            Console.WriteLine("Canciones ordenadas por duración.");
            break;

        case "6":
            Console.Write("Nombre del usuario: ");
            string nombreU = Console.ReadLine() ?? "";
            var usuario = servicio.BuscarUsuario(nombreU);
            if (usuario == null)
            {
                Console.WriteLine("Usuario no encontrado.");
                break;
            }

            Console.Write("Nombre de la nueva lista: ");
            string nombreLista = Console.ReadLine() ?? "";
            usuario.CrearListaReproduccion(nombreLista);
            break;

        case "7":
            Console.Write("Nombre del usuario: ");
            string user = Console.ReadLine() ?? "";
            var u = servicio.BuscarUsuario(user);
            if (u == null)
            {
                Console.WriteLine("Usuario no encontrado.");
                break;
            }

            Console.Write("Nombre de la lista: ");
            string lista = Console.ReadLine() ?? "";
            Console.Write("Nombre de la canción a agregar: ");
            string nombreC = Console.ReadLine() ?? "";

            var g = servicio.GetType()
                .GetField("gestor", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(servicio);

            var (coinc, _) = ((List<Cancion>, int))g.GetType().GetMethod("BuscarPorNombre").Invoke(g, new object[] { nombreC });

            if (coinc.Count > 0)
            {
                u.AgregarCancionALista(lista, coinc[0]);
            }
            else
            {
                Console.WriteLine("Canción no encontrada en el catálogo.");
            }
            break;

        case "8":
            Console.Write("Nombre del usuario: ");
            string nom = Console.ReadLine() ?? "";
            var usu = servicio.BuscarUsuario(nom);
            if (usu != null)
            {
                usu.MostrarListasReproduccion();
            }
            else
            {
                Console.WriteLine("Usuario no encontrado.");
            }
            break;

        case "9":
            salir = true;
            Console.WriteLine("Saliendo del programa...");
            break;

        default:
            Console.WriteLine("Opción inválida. Intente de nuevo.");
            break;
    }
}