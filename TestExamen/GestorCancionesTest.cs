using Examen2.Gestores;
using Examen2.Modelos;

namespace TestExamen.Test
{
    public class GestorCancionesTest
    {
        [Fact]
        public void AgregarCancion_DebeAumentarLista()
        {
            // Arrange
            var gestor = new GestorCanciones();
            var cancion = new Cancion("Imagine", "John Lennon", 183);

            // Act
            gestor.AgregarCancion(cancion);

            // Assert
            Assert.Single(gestor.CancionesDisponibles);
            Assert.Equal("Imagine", gestor.CancionesDisponibles[0].Nombre);
        }

        [Fact]
        public void BuscarPorNombre_DebeEncontrarCancion()
        {
            // Arrange
            var gestor = new GestorCanciones();
            gestor.AgregarCancion(new Cancion("Bohemian Rhapsody", "Queen", 354));
            gestor.AgregarCancion(new Cancion("Somebody to Love", "Queen", 295));

            // Act
            var (coincidencias, iteraciones) = gestor.BuscarPorNombre("Bohemian");

            // Assert
            Assert.Single(coincidencias);
            Assert.Equal("Bohemian Rhapsody", coincidencias[0].Nombre);
            Assert.True(iteraciones > 0);
        }

        [Fact]
        public void QuickSort_DebeOrdenarPorDuracion()
        {
            // Arrange
            var canciones = new List<Cancion>
            {
                new Cancion("Song A", "Artist 1", 300),
                new Cancion("Song B", "Artist 2", 150),
                new Cancion("Song C", "Artist 3", 200)
            };

            // Act
            GestorCanciones.QuickSortPorDuracion(canciones);

            // Assert
            Assert.Equal("Song B", canciones[0].Nombre);
            Assert.Equal("Song C", canciones[1].Nombre);
            Assert.Equal("Song A", canciones[2].Nombre);
        }
    }
}

