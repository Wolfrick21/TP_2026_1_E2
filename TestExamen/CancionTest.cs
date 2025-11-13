using Examen2.Modelos;

namespace TestExamen.Test
{
    public class CancionTest
    {
        [Fact]
        public void Contructor_DebeAsignarValoresCorrectamente()
        {
            // Arrange
            string nombreEsperado = "Imagine";
            string artistaEsperado = "John Lennon";
            int duracionEsperada = 183;

            // Act
            Cancion cancion = new Cancion(nombreEsperado, artistaEsperado, duracionEsperada);

            // Assert
            Assert.Equal(nombreEsperado, cancion.Nombre);
            Assert.Equal(artistaEsperado, cancion.Artista);
            Assert.Equal(duracionEsperada, cancion.DuracionSegundos);
        }

        [Fact]
        public void ToString_DebeRetornarFormatoCorrecto()
        {
            // Arrange
            Cancion cancion = new Cancion("Bohemian Rhapsody", "Queen", 354);

            // Act
            string resultado = cancion.ToString();

            // Assert
            Assert.Equal("Bohemian Rhapsody - Queen (5:54)", resultado);
        }
    }
}