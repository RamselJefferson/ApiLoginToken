using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;
using System.Linq.Expressions;

namespace PruebaTecnica.Repositories
{
    public class ModelosRepository : BaseRepository<Modelo>, IModelosRepository
    {
        public ModelosRepository(ApiContext context):base(context) 
        { 
        }


    }
}
