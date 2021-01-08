using IServicios.PuestoTrabajo.DTOs;
using System;
using System.Collections.Generic;

namespace IServicios.PuestoTrabajo
{
    public interface IPuestoTrabajoServicio : IServicio.Base.IServicio
    {
        int ObtenerSiguienteCodigo();

        

    }
}
