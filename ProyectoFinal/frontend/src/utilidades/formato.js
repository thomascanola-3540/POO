const FORMATO_FECHA = new Intl.DateTimeFormat('es-CO', { dateStyle: 'long', timeStyle: 'short' });

export function formatearFecha(texto) {
  return FORMATO_FECHA.format(new Date(texto));
}

export function formatearTamano(bytes) {
  if (bytes < 1024 * 1024) {
    return `${Math.max(1, Math.round(bytes / 1024))} KB`;
  }
  return `${(bytes / (1024 * 1024)).toFixed(1)} MB`;
}

// El campo datetime-local necesita la fecha en hora local con el formato 2026-11-04T06:15.
export function fechaParaCampo(fecha = new Date()) {
  const desfase = fecha.getTimezoneOffset() * 60000;
  return new Date(fecha.getTime() - desfase).toISOString().slice(0, 16);
}
