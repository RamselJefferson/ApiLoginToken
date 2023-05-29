using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;

namespace PruebaTecnica.Repositories
{
    public class VehiculoRepository : BaseRepository<Vehiculo>, IVehiculosRepository 
    {
        ApiContext _context;
        public VehiculoRepository(ApiContext context):base(context)
        {
            _context = context;
        }


        public int ObtenerMaxIdVeh()
        {
            var existenciaVehiculo = _context.Vehiculos.FirstOrDefault();
            var maxId = 0;

            if (existenciaVehiculo != null)
            {
                maxId = _context.Vehiculos.Max(i => i.VehId);
                return maxId;

            }
            else
            {
                return maxId;
            }
            
        }



    }
}
