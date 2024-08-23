using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_sunarp.Model;

namespace API_sunarp.Data.Dependencias
{
    public interface API_sunarp_repository
    {
        Task<IEnumerable<Datos_sunarp>> GetAllDatos();
        Task<Datos_sunarp> GetDetails(int id);
        Task<bool> InsertDatos(Datos_sunarp datos_sunarp);

        Task<bool> UpdateDatos(Datos_sunarp datos_sunarp);

        Task<bool> DeleteDatos(Datos_sunarp datos_Sunarp);

        // Nuevo método para buscar por placa
        Task<Datos_sunarp> GetVehicleByPlate(string plate);

    }
}
