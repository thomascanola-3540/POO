using Modelos;

namespace Servicios.Interfaces
{
    public interface IDataset
    {
        public string ExportarCsv(FiltroEvento filtro);
    }
}
