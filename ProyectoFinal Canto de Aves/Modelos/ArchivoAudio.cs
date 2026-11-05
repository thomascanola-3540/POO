namespace Modelos
{
    public class ArchivoAudio : Archivo
    {
        public override string Tipo => "Audio";
        protected override Dictionary<string, string> TiposPermitidos => ConfiguracionArchivos.ObtenerInstancia.TiposAudio;
        protected override long TamanoMaximoBytes => ConfiguracionArchivos.ObtenerInstancia.TamanoMaximoAudioBytes;

        public ArchivoAudio(string nombreOriginal, byte[] contenido) : base(nombreOriginal, contenido)
        {
        }
    }
}
