namespace PruebaTecnica.Interfaces
{
    public interface IUnitOfWork
    {
        public IModelosRepository IModelosRepository { get; }
        public IVehiculosRepository IVehiculosRepository { get; }
        public IvwVehiculosRepository IvwVehiculosRepository { get; }
        public IMarcasRepository IMarcasRepository { get; }
        public IvwModelosRepository IvwModelosRepository { get; }



    }
}
