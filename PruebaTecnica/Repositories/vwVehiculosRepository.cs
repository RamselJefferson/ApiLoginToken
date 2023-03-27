using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;
using PruebaTecnica.ViewModel;

namespace PruebaTecnica.Repositories
{
    public class vwVehiculosRepository : BaseRepository<vwVehiculos>, IvwVehiculosRepository

    {
        public vwVehiculosRepository(ApiContext context):base(context)
        {
            
        }
    }
}
