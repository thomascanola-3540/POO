namespace Modelos
{
    public abstract class Archivo
    {
        public int Id { get; set; }
        public string NombreOriginal { get; set; }
        public string Ruta { get; set; } = string.Empty;
        public long TamanoBytes { get; set; }
        public DateTime FechaCarga { get; set; }
        public byte[] Contenido { get; set; }

        public abstract string Tipo { get; }
        protected abstract Dictionary<string, string> TiposPermitidos { get; }
        protected abstract long TamanoMaximoBytes { get; }

        protected Archivo(string nombreOriginal, byte[] contenido)
        {
            NombreOriginal = nombreOriginal;
            Contenido = contenido;
            TamanoBytes = contenido.Length;
            FechaCarga = DateTime.Now;
        }

        public string Extension()
        {
            return Path.GetExtension(NombreOriginal).ToLower();
        }

        public string TipoContenido()
        {
            return TiposPermitidos[Extension()];
        }

        public void Validar()
        {
            if (TamanoBytes == 0)
            {
                throw new ExcepcionNegocio($"El archivo '{NombreOriginal}' está vacío.");
            }
            if (TamanoBytes > TamanoMaximoBytes)
            {
                throw new ExcepcionNegocio($"El archivo '{NombreOriginal}' supera el tamaño máximo permitido.");
            }
            if (!TiposPermitidos.ContainsKey(Extension()))
            {
                string permitidos = string.Join(", ", TiposPermitidos.Keys);
                throw new ExcepcionNegocio($"El archivo '{NombreOriginal}' no es válido. Se aceptan: {permitidos}.");
            }
        }
    }
}
