using System.Text;
using Modelos;
using Servicios.Interfaces;

namespace Servicios.Servicios
{
    public class ServicioDataset : IDataset
    {
        private readonly IEvento _evento;

        public ServicioDataset(IEvento evento)
        {
            _evento = evento;
        }

        public string ExportarCsv(FiltroEvento filtro)
        {
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("id,fecha_hora,latitud,longitud,departamento,ciudad,direccion,descripcion_lugar,notas,nombre_hipotetico,estado,ave_identificada,usuario_id,archivos");

            foreach (Evento evento in _evento.FiltrarEventos(filtro))
            {
                string rutas = string.Join(" ", evento.Archivos.Select(item => item.Ruta));
                string[] columnas =
                {
                    evento.Id.ToString(),
                    evento.FechaHora.ToString("yyyy-MM-dd HH:mm"),
                    evento.Ubicacion.Latitud.ToString(),
                    evento.Ubicacion.Longitud.ToString(),
                    Texto(evento.Ubicacion.Ciudad.Departamento.Nombre),
                    Texto(evento.Ubicacion.Ciudad.Nombre),
                    Texto(evento.Ubicacion.Direccion),
                    Texto(evento.Ubicacion.Descripcion),
                    Texto(evento.Notas),
                    Texto(evento.NombreHipotetico),
                    evento.EstadoValidacion,
                    Texto(evento.AveIdentificada?.NombreCientifico),
                    evento.Usuario?.Id.ToString() ?? "",
                    rutas
                };
                csv.AppendLine(string.Join(",", columnas));
            }

            return csv.ToString();
        }

        private string Texto(string? valor)
        {
            if (string.IsNullOrEmpty(valor))
            {
                return "";
            }
            if ("=+-@".Contains(valor[0]))
            {
                valor = "'" + valor;
            }
            return "\"" + valor.Replace("\"", "\"\"") + "\"";
        }
    }
}
