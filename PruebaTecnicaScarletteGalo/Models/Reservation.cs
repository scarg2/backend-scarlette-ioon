
using System;
namespace PruebaTecnicaScarletteGalo.Models
{
    public class Reservation
    {
        public Guid ReservationId { get; set; } // UUID como clave primaria
        public DateTime Date { get; set; } // Fecha de la reserva
        public TimeSpan StartTime { get; set; } // Hora de inicio
        public TimeSpan EndTime { get; set; } // Hora de fin
        public Guid UserId { get; set; } // Relación con la tabla Users
        public Guid SpaceId { get; set; } // Relación con la tabla Spaces
        public Guid StateId { get; set; } // Relación con la tabla States

        // Navegación
        public User User { get; set; }
        public Space Space { get; set; }
        public State State { get; set; }
    }
}

