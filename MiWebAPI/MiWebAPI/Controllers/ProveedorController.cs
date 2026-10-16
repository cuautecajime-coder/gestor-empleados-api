using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly ProveedorData _proveedorData;

        public ProveedorController(ProveedorData proveedorData)
        {
            _proveedorData = proveedorData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Proveedor> lista = await _proveedorData.Lista();
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(long id)
        {
            Proveedor? proveedor = await _proveedorData.ObtenerPorId(id);

            if (proveedor == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Proveedor no encontrado" });
            }

            return StatusCode(StatusCodes.Status200OK, proveedor);
        }

        [HttpPost]
        public async Task<IActionResult> Agrega([FromBody] Proveedor objeto)
        {
            bool respuesta = await _proveedorData.Agrega(objeto);
            if (respuesta)
                return StatusCode(StatusCodes.Status201Created, new { mensaje = "Proveedor agregado con éxito" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al agregar proveedor" });
        }

        [HttpPut]
        public async Task<IActionResult> Actualiza([FromBody] Proveedor objeto)
        {
            bool respuesta = await _proveedorData.Actualiza(objeto);
            if (respuesta)
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Proveedor actualizado con éxito" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al actualizar proveedor" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Elimina(long id)
        {
            bool respuesta = await _proveedorData.Elimina(id);
            if (respuesta)
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Proveedor eliminado con éxito" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al eliminar proveedor" });
        }
    }
}