using Modelos;

namespace Datos
{
    // Reemplaza a la base de datos: la información vive en listas estáticas mientras la API está encendida.
    public class ContextoDatos
    {
        public static List<Rol> listaRoles = new List<Rol>()
        {
            new Rol()
            {
                Id = 1,
                Nombre = RolesSistema.Administrador,
                Descripcion = "Parametriza catálogos, gestiona usuarios, filtra, valida y exporta eventos."
            },
            new Rol()
            {
                Id = 2,
                Nombre = RolesSistema.ColaboradorRegistrado,
                Descripcion = "Registra eventos, gestiona su perfil y consulta los eventos de otros usuarios."
            },
            new Rol()
            {
                Id = 3,
                Nombre = RolesSistema.ColaboradorAnonimo,
                Descripcion = "Registra eventos sin autenticarse."
            }
        };

        public static List<Usuario> listaUsuarios = new List<Usuario>()
        {
            new Usuario(1, "Administrador", "Plataforma", "admin@aves.com", ClaveSegura.Encriptar("Admin1234"), listaRoles[0]),
            new Usuario(2, "Thomas", "Cañola", "thomas@aves.com", ClaveSegura.Encriptar("Thomas1234"), listaRoles[1]),
            new Usuario(3, "Isabela", "Osorio", "isabela@aves.com", ClaveSegura.Encriptar("Isabela1234"), listaRoles[1])
        };

        public static List<Departamento> listaDepartamentos = new List<Departamento>()
        {
            new Departamento() { Id = 1, Nombre = "Antioquia" },
            new Departamento() { Id = 2, Nombre = "Cundinamarca" },
            new Departamento() { Id = 3, Nombre = "Valle del Cauca" }
        };

        public static List<Ciudad> listaCiudades = new List<Ciudad>()
        {
            new Ciudad(1, "Medellín", listaDepartamentos[0]),
            new Ciudad(2, "Rionegro", listaDepartamentos[0]),
            new Ciudad(3, "La Ceja", listaDepartamentos[0]),
            new Ciudad(4, "Bogotá", listaDepartamentos[1]),
            new Ciudad(5, "Zipaquirá", listaDepartamentos[1]),
            new Ciudad(6, "Cali", listaDepartamentos[2]),
            new Ciudad(7, "Palmira", listaDepartamentos[2])
        };

        public static List<Ave> listaAves = new List<Ave>()
        {
            new Ave() { Id = 1, NombreComun = "Mirla patiamarilla", NombreCientifico = "Turdus fuscater" },
            new Ave() { Id = 2, NombreComun = "Copetón", NombreCientifico = "Zonotrichia capensis" },
            new Ave() { Id = 3, NombreComun = "Colibrí chillón", NombreCientifico = "Colibri coruscans" },
            new Ave() { Id = 4, NombreComun = "Guacharaca colombiana", NombreCientifico = "Ortalis columbiana" }
        };

        public static List<Ubicacion> listaUbicaciones = new List<Ubicacion>();

        public static List<Archivo> listaArchivos = new List<Archivo>();

        public static List<Evento> listaEventos = new List<Evento>();
    }
}
