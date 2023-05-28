using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;
using PruebaTecnica.Repositories;
using System.Net.Http;

namespace PruebaTecnica.Unit
{
    public class UnitOfWork : IUnitOfWork
    {


        private ApiContext _context;


        public UnitOfWork(ApiContext context)
        {
            _context = context;
        }
       


    }
}
