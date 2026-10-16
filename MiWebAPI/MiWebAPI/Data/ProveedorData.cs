using MiWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MiWebAPI.Data
{
    public class ProveedorData
    {
        private readonly string conexion;

        public ProveedorData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<Proveedor>> Lista()
        {
            List<Proveedor> lista = new List<Proveedor>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("PROVEEDOR_SELECCIONA", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Proveedor
                        {
                            Id = Convert.ToInt64(reader["ID"]),
                            Nombre = reader["NOMBRE"].ToString(),
                            ApellidoPaterno = reader["APELLIDO_PATERNO"].ToString(),
                            ApellidoMaterno = reader["APELLIDO_MATERNO"].ToString(),
                            RFC = reader["RFC"].ToString(),
                            Correo = reader["CORREO"].ToString(),
                            Credito = Convert.ToDecimal(reader["CREDITO"]),
                            NombreBanco = reader["NOMBRE_BANCO"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<Proveedor?> ObtenerPorId(long id)
        {
            Proveedor? proveedor = null;

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("PROVEEDOR_SELECCIONA_ID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        proveedor = new Proveedor
                        {
                            Id = Convert.ToInt64(reader["ID"]),
                            Nombre = reader["NOMBRE"].ToString(),
                            ApellidoPaterno = reader["APELLIDO_PATERNO"].ToString(),
                            ApellidoMaterno = reader["APELLIDO_MATERNO"].ToString(),
                            RFC = reader["RFC"].ToString(),
                            Correo = reader["CORREO"].ToString(),
                            Credito = Convert.ToDecimal(reader["CREDITO"]),
                            NombreBanco = reader["NOMBRE_BANCO"].ToString()
                        };
                    }
                }
            }
            return proveedor;
        }

        public async Task<bool> Agrega(Proveedor objeto)
        {
            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("PROVEEDOR_AGREGA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NOMBRE", objeto.Nombre ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@APELLIDO_PATERNO", objeto.ApellidoPaterno ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@APELLIDO_MATERNO", objeto.ApellidoMaterno ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RFC", objeto.RFC ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CORREO", objeto.Correo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CREDITO", objeto.Credito);
                cmd.Parameters.AddWithValue("@NOMBRE_BANCO", objeto.NombreBanco ?? (object)DBNull.Value);

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }

        public async Task<bool> Actualiza(Proveedor objeto)
        {
            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("PROVEEDOR_ACTUALIZA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objeto.Id);
                cmd.Parameters.AddWithValue("@NOMBRE", objeto.Nombre ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@APELLIDO_PATERNO", objeto.ApellidoPaterno ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@APELLIDO_MATERNO", objeto.ApellidoMaterno ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RFC", objeto.RFC ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CORREO", objeto.Correo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CREDITO", objeto.Credito);
                cmd.Parameters.AddWithValue("@NOMBRE_BANCO", objeto.NombreBanco ?? (object)DBNull.Value);

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }

        public async Task<bool> Elimina(long id)
        {
            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("PROVEEDOR_ELIMINA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                return filasAfectadas > 0;
            }
        }
    }
}