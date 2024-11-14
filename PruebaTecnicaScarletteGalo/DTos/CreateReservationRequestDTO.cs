namespace PruebaTecnicaScarletteGalo.DTos
{
    public class CreateReservationRequestDTO
    {
        public Guid UserId { get; set; }
        public Guid SpaceId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

}
