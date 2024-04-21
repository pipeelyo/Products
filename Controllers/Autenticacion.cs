using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using productos.Models;
using productos.Services;

namespace productos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Autenticacion : ControllerBase
    {
        private readonly IAutorizacionService _autorizacionService;

        public Autenticacion(IAutorizacionService autorizacionService)
        {
            _autorizacionService = autorizacionService;
        }

        [HttpPost]
        [Route("Autenticar")]
        public async Task<IActionResult> Autenticar([FromBody] AutorizacionRequest autorizacion)
        {
            var resultado_autorizacion = await _autorizacionService.ReturnToken(autorizacion);
            if (resultado_autorizacion == null) return Unauthorized();

            return Ok(resultado_autorizacion);

        }
    }
}
