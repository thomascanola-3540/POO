namespace Modelos
{
    public static class RolesSistema
    {
        public const string Administrador = "Administrador";
        public const string ColaboradorRegistrado = "ColaboradorRegistrado";
        public const string ColaboradorAnonimo = "ColaboradorAnonimo";

        public const string AdministradorOColaborador = Administrador + "," + ColaboradorRegistrado;

        public static bool EsDelSistema(string nombre)
        {
            return nombre == Administrador || nombre == ColaboradorRegistrado || nombre == ColaboradorAnonimo;
        }
    }
}
