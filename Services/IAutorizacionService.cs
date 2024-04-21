using productos.Models;

namespace productos.Services
{
    public interface IAutorizacionService
    {
        Task<AutorizacionResponse> ReturnToken(AutorizacionRequest autorizacion);
    }
}
