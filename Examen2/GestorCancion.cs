namespace Examen2
{
    public class GestorCanciones
    {

        // Lista principal del catálogo

        public List<Cancion> CancionesDisponibles = new List<Cancion>();


        // Agregar canción

        public void AgregarCancion(Cancion cancion)
        {
            if (cancion == null)
            {
                Console.WriteLine("Se necesita un nombre vque no sea nulo");
                return;
            }

            CancionesDisponibles.Add(cancion);
        }

        // Buscar por nombre (coincidencias parciales)

        public (List<Cancion> coincidencias, int iteraciones) BuscarPorNombre(string nombreBuscado)
        {
            var resultados = new List<Cancion>();
            int iteraciones = 0;

            foreach (Cancion cancion in CancionesDisponibles)
            {
                iteraciones++;

                // Coincidencia parcial para no distinguir mayúsculas y minúsculas
                if (cancion.Nombre.IndexOf(nombreBuscado, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    resultados.Add(cancion);
                }
            }

            return (resultados, iteraciones);
        }

        // QuickSort por duración 

        public static void QuickSortPorDuracion(List<Cancion> canciones)
        {
            if (canciones.Count <= 1)
                return;

            // 1Seleccionar pivote (último elemento)
            Cancion pivote = canciones[canciones.Count - 1];

            // 2Dividir en menores y mayores según duración
            var menores = new List<Cancion>();
            var mayores = new List<Cancion>();

            for (int i = 0; i < canciones.Count - 1; i++)
            {
                if (canciones[i].DuracionSegundos < pivote.DuracionSegundos)
                {
                    menores.Add(canciones[i]);
                }
                else
                {
                    mayores.Add(canciones[i]);
                }
            }

            // 3️ Aplicar recursividad a sublistas
            QuickSortPorDuracion(menores);
            QuickSortPorDuracion(mayores);

            // 4️Combinar resultados
            canciones.Clear();
            canciones.AddRange(menores);
            canciones.Add(pivote);
            canciones.AddRange(mayores);
        }


        // Ordenar canciones disponibles

        public void OrdenarCancionesPorDuracion()
        {
            QuickSortPorDuracion(CancionesDisponibles);
        }


        // Mostrar canciones disponibles

        public void MostrarCancionesDisponibles()
        {
            if (CancionesDisponibles.Count == 0)
            {
                Console.WriteLine("No hay canciones disponibles.");
                return;
            }

            Console.WriteLine("Este es el catálogo de canciones:");
            foreach (var canc in CancionesDisponibles)
            {
                Console.WriteLine(canc.ToString());
            }
        }
    }
}