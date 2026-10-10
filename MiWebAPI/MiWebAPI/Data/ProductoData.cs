using MiWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MiWebAPI.Data
{
    public class ProductoData
    {

        private readonly string conexion;

        public ProductoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<ProductoDetalle>> Lista()
        {
            List<ProductoDetalle> lista = new List<ProductoDetalle>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("dbo.PRODUCTO_DETALLE_LISTAR", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new ProductoDetalle
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            NombreProducto = reader["NombreProducto"].ToString(),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            Cantidad = Convert.ToInt32(reader["Cantidad"]),
                            Total = Convert.ToDecimal(reader["Total"]),
                            Descripcion = reader["Descripcion"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

    }
}