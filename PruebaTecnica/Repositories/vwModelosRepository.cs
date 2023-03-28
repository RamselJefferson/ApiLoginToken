using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;
using PruebaTecnica.ViewModel;

namespace PruebaTecnica.Repositories
{
    public class vwModelosRepository : BaseRepository<vwModelos>, IvwModelosRepository
    {

        public vwModelosRepository(ApiContext context):base(context) 
        { 
        
        }

    }
}
