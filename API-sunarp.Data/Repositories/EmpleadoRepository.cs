using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using API_sunarp.Model;
using API_sunarp.Data.Repositories;

namespace API_sunarp.Data.Dependencias
{
   

    public class EmpleadoRepository : API_empleadoRepository
    {
        private readonly MySqlConfiguration _connectionString;

        public EmpleadoRepository(MySqlConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        // Método para obtener el empleado por credenciales
        public async Task<Empleado> GetEmpleadoByCredentials(string nombre, string clave)
        {
            using var connection = dbConnection();
            var sql = "SELECT * FROM empleado WHERE TRIM(nombre) = @Nombre AND TRIM(clave) = @Clave";
            return await connection.QueryFirstOrDefaultAsync<Empleado>(sql, new { Nombre = nombre.Trim(), Clave = clave.Trim() });
        }


        public async Task<bool> DeleteDatos(Empleado empleado)
        {
            var db = dbConnection();
            var sql = @"DELETE FROM empleado WHERE id = @Id";
            var result = await db.ExecuteAsync(sql, new { Id = empleado.id });
            return result > 0;
        }

        public async Task<IEnumerable<Empleado>> GetAllDatos()
        {
            var db = dbConnection();
            var sql = @"SELECT id, nombre, clave FROM empleado";
            return await db.QueryAsync<Empleado>(sql);
        }

        public async Task<Empleado> GetDetails(int id)
        {
            var db = dbConnection();
            var sql = @"SELECT id, nombre, clave FROM empleado WHERE id = @Id";
            return await db.QueryFirstOrDefaultAsync<Empleado>(sql, new { Id = id });
        }

        public async Task<bool> InsertDatos(Empleado empleado)
        {
            var db = dbConnection();
            var sql = @"INSERT INTO empleado(nombre, clave) VALUES(@Nombre, @Clave)";
            var result = await db.ExecuteAsync(sql, new { empleado.nombre, empleado.clave });
            return result > 0;
        }

        public async Task<bool> UpdateDatos(Empleado empleado)
        {
            var db = dbConnection();
            var sql = @"UPDATE empleado SET nombre=@Nombre, clave=@Clave WHERE id = @Id";
            var result = await db.ExecuteAsync(sql, new { empleado.nombre, empleado.clave, empleado.id });
            return result > 0;
        }
    }
}
