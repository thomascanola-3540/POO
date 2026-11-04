namespace Modelos
{
    public class ArchivoImagen : Archivo
    {
        public override string Tipo => "Imagen";
        protected override Dictionary<string, string> TiposPermitidos => ConfiguracionArchivos.ObtenerInstancia.TiposImagen;
        protected override long TamanoMaximoBytes => ConfiguracionArchivos.ObtenerInstancia.TamanoMaximoImagenBytes;

        public ArchivoImagen(string nombreOriginal, byte[] contenido) : base(nombreOriginal, contenido)
        {
        }
    }
}
