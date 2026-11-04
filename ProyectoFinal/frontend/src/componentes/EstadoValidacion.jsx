const ETIQUETAS = { Pendiente: 'Por validar', Validado: 'Validado', Rechazado: 'Rechazado' };

export default function EstadoValidacion({ estado }) {
  return <span className={`insignia insignia-${estado.toLowerCase()}`}>{ETIQUETAS[estado] ?? estado}</span>;
}
