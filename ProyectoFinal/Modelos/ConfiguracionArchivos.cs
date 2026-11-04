namespace Modelos
{
    // Patrón Singleton: las reglas de los archivos (RN10) son las mismas para todo el sistema,
    // así que solo existe una instancia de esta clase.
    public class ConfiguracionArchivos
    {
        private static ConfiguracionArchivos? _instancia;

        private ConfiguracionArchivos()
        {
        }

        public static ConfiguracionArchivos ObtenerInstancia => _instancia ??= new ConfiguracionArchivos();

        public long TamanoMaximoAudioBytes { get; } = 15 * 1024 * 1024; // 15 MB
        public long TamanoMaximoImagenBytes { get; } = 2 * 1024 * 1024; // 2 MB
        public int MaximoImagenesPorEvento { get; } = 5;

        // Extensión permitida -> tipo de contenido con el que se entrega el archivo
        public Dictionary<string, string> TiposAudio { get; } = new Dictionary<string, string>()
        {
            { ".mp3", "audio/mpeg" },
            { ".wav", "audio/wav" },
            { ".ogg", "audio/ogg" },
            { ".m4a", "audio/mp4" }
        };

        public Dictionary<string, string> TiposImagen { get; } = new Dictionary<string, string>()
        {
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".png", "image/png" }
        };
    }
}
