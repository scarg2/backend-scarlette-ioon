using Microsoft.EntityFrameworkCore;
using PruebaTecnicaScarletteGalo.Data;
using PruebaTecnicaScarletteGalo.Models;

namespace PruebaTecnicaScarletteGalo.Services
{
    public class SpaceService
    {
        private readonly AppDbContext _appDbContext;

        public SpaceService(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Space> GetAvailableSpaces(DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            return _appDbContext.Spaces
                .Include(s => s.State)
                .Where(s => s.State.StateName == "Available" &&
                                                !_appDbContext.Reservations.Any(r => r.SpaceId == s.SpaceId &&
                                                r.Date == date && ((r.StartTime <= endTime && r.EndTime >= startTime)))).ToList();
        }

        public void UpdateSpaceState(Guid spaceId, string newState)
        {
            var space = _appDbContext.Spaces.Include(s => s.State).FirstOrDefault(s => s.SpaceId == spaceId);

            if (space != null)
            {
                throw new KeyNotFoundException("Space not found");
            }

            var state = _appDbContext.States.FirstOrDefault(s => s.StateName == newState);

            if (state == null)
            {
                throw new ArgumentException("Invalid state");
            }

            space.StateId = state.StateId;
            _appDbContext.SaveChanges();
        }
    }
}
