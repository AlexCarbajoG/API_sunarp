using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_sunarp.Model;
using Dapper;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace API_sunarp.Data.Dependencias
{
    public class Sunarp_repository : API_sunarp_repository
    {
        private readonly MySqlConfiguration _connectionString;
        public Sunarp_repository(MySqlConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteDatos(Datos_sunarp datos_Sunarp)
        {
            var db = dbConnection();

            var sql = @"DELETE FROM sunarp_api2 WHERE id = @Id";

            var result = await db.ExecuteAsync(sql,new { Id = datos_Sunarp.Id });

            return result > 0;
             
        }

        public async Task<IEnumerable<Datos_sunarp>> GetAllDatos()
        {
            var db = dbConnection();

            var sql = @" SELECT  id, owner, plate, phone_number, mail, address, number_engine, serial_number, model, category, brand, color, status FROM sunarp_api2";

            return await db.QueryAsync<Datos_sunarp>(sql, new { });

        }

        public async Task<Datos_sunarp> GetDetails(int id)
        {
            var db = dbConnection();

            var sql = @" SELECT  id, owner, plate, phone_number, mail, address, number_engine, serial_number, model, category, brand, color, status FROM sunarp_api2 WHERE id = @Id ";
                        
            return await db.QueryFirstOrDefaultAsync<Datos_sunarp>(sql, new { Id = id });         
        }

        public async Task<bool> InsertDatos(Datos_sunarp datos_sunarp)
        {
            var db = dbConnection();

            var sql = @" INSERT INTO sunarp_api2(owner, plate, phone_number, mail, address, number_engine, serial_number, model, category, brand, color, status)
                        VALUES(@owner, @plate, @phone_number, @mail, @address, @number_engine, @serial_number, @model, @category, @brand, @color, @status) ";

            var result = await db.ExecuteAsync(sql, new { datos_sunarp.Owner, datos_sunarp.Plate, datos_sunarp.Phone_number, datos_sunarp.Mail, datos_sunarp.Address, datos_sunarp.Number_engine, datos_sunarp.Serial_number, datos_sunarp.Model, datos_sunarp.Category, datos_sunarp.Brand, datos_sunarp.Color, datos_sunarp.Status });

            return result > 0;


        }

        public async Task<bool> UpdateDatos(Datos_sunarp datos_sunarp)
        {
            var db = dbConnection();

            var sql = @"  UPDATE sunarp_api2 SET owner=@Owner, plate=@Plate, phone_number=@Phone_number, mail=@Mail, address=@Address, number_engine=@Number_engine, serial_number=@Serial_number, model=@Model, category=@Category, brand=@Brand, color=@Color, status=@Status WHERE id = @Id";
                         
            var result = await db.ExecuteAsync(sql, new { datos_sunarp.Owner, datos_sunarp.Plate, datos_sunarp.Phone_number, datos_sunarp.Mail, datos_sunarp.Address, datos_sunarp.Number_engine, datos_sunarp.Serial_number, datos_sunarp.Model, datos_sunarp.Category, datos_sunarp.Brand, datos_sunarp.Color, datos_sunarp.Status, datos_sunarp.Id});

            return result > 0;
        }
    }
}
