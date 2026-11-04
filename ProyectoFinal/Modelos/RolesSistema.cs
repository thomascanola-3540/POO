namespace Modelos
{
    // Nombres de los tres roles del sistema (RN04). Se usan en los [Authorize] de los controladores.
    public static class RolesSistema
    {
        public const string Administrador = "Administrador";
        public const string ColaboradorRegistrado = "ColaboradorRegistrado";
        public const string ColaboradorAnonimo = "ColaboradorAnonimo";

        // Para consultar eventos y reproducir audios (RF08 y RF09).
        public const string AdministradorOColaborador = Administrador + "," + ColaboradorRegistrado;

        public static bool EsDelSistema(string nombre)
        {
            return nombre == Administrador || nombre == ColaboradorRegistrado || nombre == ColaboradorAnonimo;
        }
    }
}
