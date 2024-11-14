using Microsoft.EntityFrameworkCore;
using PruebaTecnicaScarletteGalo.Data;
using PruebaTecnicaScarletteGalo.Models;

namespace PruebaTecnicaScarletteGalo.Services
{
    public class ReservationService
    {
        private readonly AppDbContext _appDbContext;

        public ReservationService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Reservation CreateReservation (Guid userId, Guid spaceId, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var user = _appDbContext.Users.Include(u => u.State).FirstOrDefault(u => u.UserId == userId);
            var space = _appDbContext.Spaces.Include(s => s.State).FirstOrDefault(s => s.SpaceId == spaceId);

            if (user == null || user.State.StateName != "Active")
                throw new UnauthorizedAccessException("User not found or inactive");

            if (space == null || space.State.StateName != "Available")
                throw new InvalidOperationException("Space is not available");

            if (_appDbContext.Reservations.Any(r => r.SpaceId == spaceId && r.Date == date && ((r.StartTime <= endTime && r.EndTime >= startTime))))
                throw new InvalidOperationException("Space is already reserved for the selected time.");

            var reservation = new Reservation
            {
                ReservationId = Guid.NewGuid(),
                UserId = userId,
                SpaceId = spaceId,
                Date = date,
                StartTime = startTime,
                EndTime = endTime,
                StateId = _appDbContext.States.First(s => s.StateName == "Active").StateId
            };

            _appDbContext.Reservations.Add(reservation);
            _appDbContext.SaveChanges();

            return reservation;
        }

        public IEnumerable<Reservation> GetReservationByUser (Guid userId)
        {
            return _appDbContext.Reservations.Include(r => r.Space)
                                             .Include(r => r.State)
                                             .Where(r => r.UserId == userId)
                                             .ToList();
        }

        public IEnumerable<Reservation> GetActiveReservationBySpace(Guid spaceId)
        {
            return _appDbContext.Reservations.Include(r => r.User)
                                             .Include(r => r.State)
                                             .Where(r => r.SpaceId == spaceId && r.State.StateName == "Active")
                                             .ToList();
        }

        public void CancelReservation(Guid reservationId)
        {
            var reservation = _appDbContext.Reservations.Include(r => r.State).FirstOrDefault(r => r.ReservationId == reservationId);

            if (reservation != null)
            {
                throw new KeyNotFoundException("Reservation not found");
            }

            reservation.StateId = _appDbContext.States.First(s => s.StateName == "Canceled").StateId;
            _appDbContext.SaveChanges();
        }
    }
}
