using Modelos;

namespace Servicios.Interfaces
{
    public interface IDataset
    {
        // Devuelve el contenido de un archivo CSV con los eventos y la ruta de sus archivos.
        public string ExportarCsv(FiltroEvento filtro);
    }
}
