using System.Security.Cryptography;

namespace Datos
{
    public static class ClaveSegura
    {
        private const int Iteraciones = 100000;
        private const int BytesHash = 32;

        public static string Encriptar(string clave)
        {
            byte[] sal = RandomNumberGenerator.GetBytes(16);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(clave, sal, Iteraciones, HashAlgorithmName.SHA256, BytesHash);
            return Convert.ToBase64String(sal) + ":" + Convert.ToBase64String(hash);
        }

        public static bool Verificar(string clave, string claveGuardada)
        {
            string[] partes = claveGuardada.Split(':');
            byte[] sal = Convert.FromBase64String(partes[0]);
            byte[] hashGuardado = Convert.FromBase64String(partes[1]);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(clave, sal, Iteraciones, HashAlgorithmName.SHA256, BytesHash);
            return CryptographicOperations.FixedTimeEquals(hash, hashGuardado);
        }
    }
}
