using IServicio.Base;

namespace IServicio.Usuario
{
    public interface IUsuarioServicio : IServicioConsulta
    {
        void Crear(long empleadoId, string apellidoEmpleado, string nombreEmpleado);

        void Bloquear(long usuarioId);

        void ResetPassword(long usuarioId);

        void CambiarPassword(long usuarioId, string password);
    }
}
