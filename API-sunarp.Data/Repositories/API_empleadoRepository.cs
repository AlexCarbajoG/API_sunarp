using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_sunarp.Model;

namespace API_sunarp.Data.Repositories
{
    public interface API_empleadoRepository
    {
        Task<Empleado> GetEmpleadoByCredentials(string nombre, string clave);
        Task<bool> DeleteDatos(Empleado empleado);
        Task<IEnumerable<Empleado>> GetAllDatos();
        Task<Empleado> GetDetails(int id);
        Task<bool> InsertDatos(Empleado empleado);
        Task<bool> UpdateDatos(Empleado empleado);
    }
}
