using Examen2.Modelos;
namespace Examen2.Servicios
{
    public class ServicioMusica
    {
        //Propiedades
        private GestorCanciones gestor = new GestorCanciones();
        private List<Usuario> usuarios = new List<Usuario>();


        public void RegistrarUsuario(string nombre)
        {

            Usuario nuevo = new Usuario(nombre);
            usuarios.Add(nuevo);
            Console.WriteLine($"Usuario {nombre} registrado correctamente.");
        }

        // BuscarUsuario por nombre 
        public Usuario BuscarUsuario(string nombre)
        {
            foreach (var usua in usuarios)
            {
                if (usua.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    return usua;
                }
            }

            return null;
        }
    }
}
