using Examen2.Modelos;
using Examen2.Servicios;

namespace TestExamen.Test
{
    public class ServicioMusicaTest
    {
        [Fact]
        public void RegistrarUsuario_DebeAgregarNuevoUsuario()
        {
            // Arrange
            ServicioMusica servicio = new ServicioMusica();
            string nombreUsuario = "Erick";

            // Act
            servicio.RegistrarUsuario(nombreUsuario);
            Usuario usuarioEncontrado = servicio.BuscarUsuario(nombreUsuario);

            // Assert
            Assert.NotNull(usuarioEncontrado);
            Assert.Equal(nombreUsuario, usuarioEncontrado.Nombre);
        }

        [Fact]
        public void BuscarUsuario_DebeRetornarUsuarioExistente()
        {
            // Arrange
            ServicioMusica servicio = new ServicioMusica();
            string nombreUsuario = "Laura";
            servicio.RegistrarUsuario(nombreUsuario);

            // Act
            Usuario resultado = servicio.BuscarUsuario("Laura");

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Laura", resultado.Nombre);
        }

        [Fact]
        public void BuscarUsuario_DebeRetornarNullSiNoExiste()
        {
            // Arrange
            ServicioMusica servicio = new ServicioMusica();

            // Act
            Usuario resultado = servicio.BuscarUsuario("Inexistente");

            // Assert
            Assert.Null(resultado);
        }
    }
}
