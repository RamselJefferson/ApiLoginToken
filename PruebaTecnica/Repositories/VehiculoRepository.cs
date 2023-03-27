using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;

namespace PruebaTecnica.Repositories
{
    public class VehiculoRepository : BaseRepository<Vehiculo>, IVehiculosRepository 
    {
        public VehiculoRepository(ApiContext context):base(context)
        {
            
        }
    }
}
