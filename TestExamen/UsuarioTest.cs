using Examen2.Modelos;
using Xunit;

namespace TestExamen.Test
{
    public class UsuarioTest
    {
        [Fact]
        public void CrearListaReproduccion_DebeCrearNuevaLista()
        {
            // Arrange
            Usuario usuario = new Usuario("Erick");

            // Act
            usuario.CrearListaReproduccion("Favoritas");

            // Assert
            Assert.True(usuario.ListasReproduccion.ContainsKey("Favoritas"));
            Assert.Empty(usuario.ListasReproduccion["Favoritas"]);
        }

        [Fact]
        public void AgregarCancionALista_DebeAgregarCancionAListaExistente()
        {
            // Arrange
            Usuario usuario = new Usuario("Erick");
            usuario.CrearListaReproduccion("Rock");
            Cancion cancion = new Cancion("Back In Black", "AC/DC", 255);

            // Act
            usuario.AgregarCancionALista("Rock", cancion);

            // Assert
            Assert.Single(usuario.ListasReproduccion["Rock"]);
            Assert.Equal("Back In Black", usuario.ListasReproduccion["Rock"][0].Nombre);
        }

        [Fact]
        public void AgregarCancionALista_NoDebeAgregarSiListaNoExiste()
        {
            // Arrange
            Usuario usuario = new Usuario("Erick");
            Cancion cancion = new Cancion("Imagine", "John Lennon", 183);

            // Act
            usuario.AgregarCancionALista("Inexistente", cancion);

            // Assert
            Assert.Empty(usuario.ListasReproduccion);
        }
    }
}

