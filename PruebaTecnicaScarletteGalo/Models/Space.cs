
using System;
namespace PruebaTecnicaScarletteGalo.Models
{
    public class Space
    {
        public Guid SpaceId { get; set; } // UUID como clave primaria
        public string SpaceName { get; set; } // Nombre del espacio
        public int Capacity { get; set; } // Capacidad máxima
        public Guid StateId { get; set; } // Relación con la tabla States

        // Navegación
        public State State { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
