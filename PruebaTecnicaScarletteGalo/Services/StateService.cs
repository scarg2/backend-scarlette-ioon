using PruebaTecnicaScarletteGalo.Data;
using PruebaTecnicaScarletteGalo.Models;

namespace PruebaTecnicaScarletteGalo.Services
{
    public class StateService
    {
        private readonly AppDbContext _appContext;

        public StateService (AppDbContext appContext)
        {
            _appContext = appContext;
        }

        public IEnumerable<State> GetAllStates()
        {
            return _appContext.States.ToList();
        }
    }
}
