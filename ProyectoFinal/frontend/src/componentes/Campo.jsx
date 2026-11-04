// Etiqueta, control y ayuda de un campo de formulario. El control va como hijo.
export default function Campo({ etiqueta, ayuda, opcional, children }) {
  return (
    <label className="campo">
      <span className="campo-etiqueta">
        {etiqueta}
        {opcional && <span className="campo-opcional"> (opcional)</span>}
      </span>
      {children}
      {ayuda && <span className="campo-ayuda">{ayuda}</span>}
    </label>
  );
}
