using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;
using PruebaTecnica.Repositories;
using System.Net.Http;

namespace PruebaTecnica.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
        private IMarcasRepository _IMarcasRepository;

        private IModelosRepository _IModelosRepository;

        private IvwModelosRepository _IvwModelosRepository;

        private IVehiculosRepository _IVehiculosRepository;

        private IvwVehiculosRepository _IvwVehiculosRepository;

        private ApiContext _context;


        public UnitOfWork(ApiContext context)
        {
            _context = context;
        }
        public IMarcasRepository IMarcasRepository
        {
            get
            {
                return _IMarcasRepository == null ? _IMarcasRepository = new MarcasRepository(_context) : _IMarcasRepository;
            }
        }

        public IvwVehiculosRepository IvwVehiculosRepository
        {
            get
            {
                return _IvwVehiculosRepository ??= new vwVehiculosRepository(_context);
            }
        }
        public IVehiculosRepository IVehiculosRepository
        {
            get
            {
                return _IVehiculosRepository == null ? _IVehiculosRepository = new VehiculoRepository(_context) : _IVehiculosRepository;
            }
        }

        public IModelosRepository IModelosRepository
        {

            get
            {
                return _IModelosRepository == null ? _IModelosRepository = new ModelosRepository(_context) : _IModelosRepository;
            }
        }


        public IvwModelosRepository IvwModelosRepository
        {

            get
            {
                return _IvwModelosRepository == null ? _IvwModelosRepository = new vwModelosRepository(_context) : _IvwModelosRepository;
            }
        }

        //IModelosRepository IUnitOfWork._IModelosRepository => throw new NotImplementedException();

        //IVehiculosRepository IUnitOfWork._IVehiculosRepository => throw new NotImplementedException();

        //IvwVehiculosRepository IUnitOfWork._IvwVehiculosRepository => throw new NotImplementedException();

        //IMarcasRepository IUnitOfWork._IMarcasRepository => throw new NotImplementedException();

        //IvwModelosRepository IUnitOfWork._IvwModelosRepository => throw new NotImplementedException();
    }
}
