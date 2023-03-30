using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;

namespace PruebaTecnica.Repositories
{
    public class MarcasRepository : BaseRepository<Marca>, IMarcasRepository
    {
        public MarcasRepository(ApiContext context) : base(context)
        {

        }
    }
}
