namespace Modelos
{
    // Criterios con los que el administrador consulta los eventos (RF13).
    public class FiltroEvento
    {
        public int? DepartamentoId { get; set; }
        public int? CiudadId { get; set; }
        public int? AveId { get; set; }
        public int? UsuarioId { get; set; }

        public List<Evento> Aplicar(List<Evento> eventos)
        {
            return eventos.Where(item =>
                (DepartamentoId == null || item.Ubicacion.Ciudad.Departamento.Id == DepartamentoId) &&
                (CiudadId == null || item.Ubicacion.Ciudad.Id == CiudadId) &&
                (AveId == null || (item.AveIdentificada != null && item.AveIdentificada.Id == AveId)) &&
                (UsuarioId == null || (item.Usuario != null && item.Usuario.Id == UsuarioId))
            ).ToList();
        }
    }
}
